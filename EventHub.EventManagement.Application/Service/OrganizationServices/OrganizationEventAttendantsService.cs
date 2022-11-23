using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationEventAttendantsService : IOrganizationEventAttendantsService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationEventAttendantsService
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
         (Guid organizationId, Guid eventId, AttendantForCreationDto attendantForCreationDto, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var attendant = _mapper
            .Map<Attendant>(attendantForCreationDto);

         _repository
            .AttendantRepository
            .CreateAttendant(eventId, attendant);

         await _repository.SaveAsync();

         var attendantToReturn = _mapper
            .Map<AttendantDto>(attendant);

         return attendantToReturn;
      }

      public async Task<(LinkResponse link, MetaData metaData)> GetAllAttendantsAsync
         (Guid organizationId, Guid eventId, AttendantLinkParams attendantLinkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var attendantsWithMetaData = await _repository
            .AttendantRepository
            .GetAllAttendantsAsync(eventId, attendantLinkParams.attendantParams, trackChanges);

         var attendantsDto = _mapper
            .Map<IEnumerable<AttendantDto>>(attendantsWithMetaData);

         var link = _entitiesLinkGenerator.OrganizationEventAttendantLinks
            .TryGetEntitiesLinks(attendantsDto,
                                 attendantLinkParams.attendantParams.Fields!,
                                 attendantLinkParams.HttpContext,
                                 eventId,
                                 organizationId);

         return (link, metaData: attendantsWithMetaData.MetaData);

      }

      public async Task<LinkResponse> GetAttendantAsync
         (Guid organizationId, Guid eventId, Guid attendantId, AttendantLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var attendant = await GetAttendantAndCheckIfItExists
            (eventId, attendantId, trackChanges);

         var attendantDto = _mapper
            .Map<AttendantDto>(attendant);

         var linkResponse = _entitiesLinkGenerator.OrganizationEventAttendantLinks
            .TryGetEntityLinks(attendantDto,
                               linkParams.attendantParams.Fields!,
                               linkParams.HttpContext,
                               eventId,
                               organizationId);

         return linkResponse;
      }

      public async Task RemoveAttendantAsync
         (Guid organizationId, Guid eventId, Guid attendantId, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var attendant = await GetAttendantAndCheckIfItExists
            (eventId, attendantId, trackChanges);

         _repository
            .AttendantRepository
            .RemoveAttendant(attendant);

         await _repository.SaveAsync();
      }


      private async Task CheckIfOrganizationExists
         (Guid organizationId, bool trackChanges)
      {
         var organization = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges);

         if (organization is null)
            throw new OrganizationNotFoundException("id", organizationId);
      }

      private async Task CheckIfOrganizationEventExists
         (Guid organizationId, Guid eventId, bool trackChanges)
      {
         var organizationEvent = await _repository
            .OrganizationEventsRepository
            .GetOrganizationEventAsync(organizationId, eventId, trackChanges);

         if (organizationEvent is null)
            throw new EventNotFoundException("id", eventId);
      }

      private async Task<Attendant> GetAttendantAndCheckIfItExists
         (Guid eventId, Guid attendantId, bool trackChanges)
      {
         var attendant = await _repository
            .AttendantRepository
            .GetAttendantAsync(eventId, attendantId, trackChanges);

         if (attendant is null)
            throw new AttendantNotFoundException("id", attendantId);

         return attendant;
      }
   }

}
