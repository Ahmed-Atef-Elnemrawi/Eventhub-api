using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.ProducerServices;
using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Service.ProducerServices
{
   internal sealed class ProducerEventAttendantsService : IProducerEventAttendantsService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public ProducerEventAttendantsService
         (
         IRepositoryManager repository,
         ILoggerManager logger,
         IMapper mapper,
         IEntitiesLinkGeneratorManager entitiesLinkGenerator)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = entitiesLinkGenerator ?? throw new ArgumentNullException(nameof(entitiesLinkGenerator));
      }

      public async Task<AttendantDto> CreateAttendantAsync
         (Guid producerId, Guid eventId, AttendantForCreationDto attendantForCreationDto, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);
         await CheckIfProducerEventExists(producerId, eventId, trackChanges);

         var attendantEntity = _mapper
            .Map<Attendant>(attendantForCreationDto);

         _repository
            .AttendantRepository
            .CreateAttendant(eventId, attendantEntity);

         await _repository.SaveAsync();

         var attendantToReturn = _mapper
            .Map<AttendantDto>(attendantEntity);

         return attendantToReturn;
      }

      public async Task<(LinkResponse link, MetaData metaData)>
         GetAllAttendantsAsync
         (Guid producerId, Guid eventId, AttendantLinkParams linkParams, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);
         await CheckIfProducerEventExists(producerId, eventId, trackChanges);

         var attendantsWithMetaData = await _repository
            .AttendantRepository
            .GetAllAttendantsAsync(eventId, linkParams.attendantParams, trackChanges);

         var attendantsDto = _mapper
            .Map<IEnumerable<AttendantDto>>(attendantsWithMetaData);

         var link = _entitiesLinkGenerator.ProducerEventAttendantLinks
            .TryGetEntitiesLinks(attendantsDto,
                                 linkParams.attendantParams.Fields!,
                                 linkParams.HttpContext,
                                 eventId,
                                 producerId);

         return (link, metaData: attendantsWithMetaData.MetaData);
      }

      public async Task<LinkResponse> GetAttendantAsync
         (Guid producerId, Guid eventId, Guid attendantId, AttendantLinkParams linkParams, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);
         await CheckIfProducerEventExists(producerId, eventId, trackChanges);

         var attendant = await GetAttendantAndCheckIfItExists
            (eventId, attendantId, trackChanges);

         var attendantDto = _mapper
            .Map<AttendantDto>(attendant);

         var linkResponse = _entitiesLinkGenerator.ProducerEventAttendantLinks
            .TryGetEntityLinks(attendantDto,
                               linkParams.attendantParams.Fields!,
                               linkParams.HttpContext,
                               eventId,
                               producerId);

         return linkResponse;
      }

      public async Task RemoveAttendantAsync
         (Guid producerId, Guid eventId, Guid attendantId, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);
         await CheckIfProducerEventExists(producerId, eventId, trackChanges);

         var attendant = await GetAttendantAndCheckIfItExists
            (eventId, attendantId, trackChanges);

         _repository
            .AttendantRepository
            .RemoveAttendant(attendant);

         await _repository.SaveAsync();
      }

      private async Task CheckIfProducerExists(Guid producerId, bool trackChanges)
      {
         var producer = await _repository
            .ProducerRepository
            .GetProducerAsync(producerId, trackChanges);

         if (producer is null)
            throw new ProducerNotFound("id", producerId);
      }

      private async Task CheckIfProducerEventExists(Guid producerId, Guid eventId, bool trackChanges)
      {
         var producerEvent = await _repository
            .ProducerEventsRepository
            .GetProducerEventAsync(producerId, eventId, trackChanges);

         if (producerEvent is null)
            throw new EventNotFound("id", eventId);
      }

      private async Task<Attendant> GetAttendantAndCheckIfItExists
         (Guid eventId, Guid attendantId, bool trackChanges)
      {
         var attendant = await _repository
            .AttendantRepository
            .GetAttendantAsync(eventId, attendantId, trackChanges);

         if (attendant is null)
            throw new AttendantNotFound("id", attendantId);

         return attendant;
      }
   }
}
