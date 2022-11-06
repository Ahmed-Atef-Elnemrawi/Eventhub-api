using AutoMapper;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Logging;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.ProducerServices;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Service.ProducerServices
{
   internal sealed class ProducerEventService : IProducerEventService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;


      public ProducerEventService
         (IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IEntitiesLinkGeneratorManager linksManager)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = linksManager ?? throw new ArgumentNullException(nameof(linksManager));

      }

      public async Task<EventDto> CreateProducerEventAsync
         (Guid producerId, EventForCreationDto producerEvent, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var producerEventEntity = _mapper
            .Map<ProducerEvent>(producerEvent);

         _repository
            .ProducerEventsRepository
            .CreateProducerEvent(producerId, producerEventEntity);

         await _repository.SaveAsync();

         var ProducerEventToReturn = _mapper
            .Map<EventDto>(producerEventEntity);

         return ProducerEventToReturn;
      }

      public async Task<(LinkResponse linkResponse, MetaData metaData)>
         GetAllProducerEventsAsync
         (Guid producerId, EventLinkParams linkParam, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var eventsWithMetaData = await _repository
            .ProducerEventsRepository
            .GetAllProducerEventsAsync(producerId, linkParam.eventParams, trackChanges);

         var eventsDto = _mapper
            .Map<IEnumerable<EventDto>>(eventsWithMetaData);


         var linkResponse =
            _entitiesLinkGenerator.ProducerEventLinks.TryGetEntitiesLinks
               (eventsDto,
                linkParam.eventParams.Fields!,
                linkParam.HttpContext,
                producerId);

         return (linkResponse, metaData: eventsWithMetaData.MetaData);
      }

      public async Task<LinkResponse> GetProducerEventAsync
         (Guid producerId, Guid eventId, EventLinkParams linkParams, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var @event =
            await GetProducerEventAndCheckIfItExists(producerId, eventId, trackChanges);

         var eventDto = _mapper
            .Map<EventDto>(@event);

         var linkResponse = _entitiesLinkGenerator.ProducerEventLinks
            .TryGetEntityLinks(eventDto, linkParams.eventParams.Fields!, linkParams.HttpContext, producerId);

         return linkResponse;
      }

      public async Task RemoveProducerEventAsync
         (Guid producerId, Guid eventId, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var @event =
            await GetProducerEventAndCheckIfItExists(producerId, eventId, trackChanges);

         _repository
            .ProducerEventsRepository
            .RemoveProducerEvent(@event);

         await _repository.SaveAsync();
      }

      public async Task UpdateProducerEventAsync(Guid producerId, Guid eventId,
         EventForUpdateDto eventForUpdate, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var @event =
            await GetProducerEventAndCheckIfItExists(producerId, eventId, trackChanges);

         _mapper.Map(eventForUpdate, @event);

         await _repository.SaveAsync();
      }

      private async Task CheckIfProducerExists(Guid producerId, bool trackChanges)
      {
         var producer = await _repository
            .ProducerRepository
            .GetProducerAsync(producerId, trackChanges: false);

         if (producer is null)
            throw new ProducerNotFoundException("id", producerId);
      }

      private async Task<ProducerEvent> GetProducerEventAndCheckIfItExists(Guid producerId, Guid eventId,
         bool trackChanges)
      {
         var @event = await _repository
           .ProducerEventsRepository
           .GetProducerEventAsync(producerId, eventId, trackChanges);

         if (@event is null)
            throw new EventNotFoundException("id", eventId);

         return @event;
      }
   }
}
