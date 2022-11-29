using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.DataShaperService;
using EventHub.EventManagement.Application.Contracts.Service.EventServices;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;

namespace EventHub.EventManagement.Application.Service.EventServices
{
   internal sealed class EventService : IEventService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;
      private readonly IDataShaperManager _dataShaper;

      public EventService
         (IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IEntitiesLinkGeneratorManager entitiesLinkGenerator, IDataShaperManager dataShaper)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = entitiesLinkGenerator ?? throw new ArgumentNullException(nameof(entitiesLinkGenerator));
         _dataShaper = dataShaper;
      }

      public async Task<(LinkResponse linkResponse, MetaData metaData)>
         GetAllCategoryEventsAsync(Guid mediumId, Guid categoryId, EventLinkParams linkParams, bool trackChanges)
      {

         var eventsWithMetaData = await _repository
            .EventRepository
            .GetCategoryEventsAsync(categoryId, linkParams.eventParams, trackChanges);

         var eventsDto = _mapper
            .Map<IEnumerable<EventDto>>(eventsWithMetaData);

         var linkResponse = _entitiesLinkGenerator.EventLinks
            .TryGetEntitiesLinks(eventsDto,
                                 linkParams.eventParams.Fields!,
                                 linkParams.HttpContext,
                                 categoryId,
                                 mediumId);

         return (linkResponse, metaData: eventsWithMetaData.MetaData);
      }

      public async Task<LinkResponse>
         GetCategoryEventAsync(Guid mediumId, Guid categoryId,
         Guid eventId, EventLinkParams linkParams, bool trackChanges)
      {
         var category = await _repository
            .CategoryRepository
            .GetCategoryAsync(categoryId, trackChanges);

         if (category is null)
            throw new CategoryNotFound("id", categoryId);

         var producerEvent = await _repository
            .EventRepository
            .GetCategoryProducerEventAsync(categoryId, eventId, trackChanges);

         var organizationEvent = await _repository
            .EventRepository
            .GetCategoryOrganizationEventAsync(categoryId, eventId, trackChanges);

         if (producerEvent is null && organizationEvent is null)
            throw new EventNotFound("id", eventId);

         var producerEventDto = _mapper.Map<ProducerEventDto>(producerEvent);
         var organizationEventDto = _mapper.Map<OrganizationEventDto>(organizationEvent);


         if (organizationEvent is not null)
         {
            return _entitiesLinkGenerator.CategoryOrganizationEventLinks
           .TryGetEntityLinks(organizationEventDto,
                              linkParams.eventParams.Fields!,
                              linkParams.HttpContext,
                              organizationEventDto.OrganizationId);

         }
         return _entitiesLinkGenerator.CategoryProducerEventLinks
            .TryGetEntityLinks(producerEventDto,
                               linkParams.eventParams.Fields!,
                               linkParams.HttpContext,
                               producerEventDto.ProducerId);

      }

      public async Task<EventDto?> GetEventAsync(Guid eventId, bool trackChanges)
      {
         var eventEntity = await _repository
            .EventRepository
            .GetEventAsync(eventId, trackChanges);

         if (eventEntity is null)
            throw new EventNotFound("id", eventId);

         var eventToReturn = _mapper
            .Map<EventDto>(eventEntity);

         return eventToReturn;
      }

      public async Task<(IEnumerable<ShapedEntity> events, MetaData metaData)>
         GetAllEventsAsync(EventParams eventParams, bool trackChanges)
      {
         var eventWithMetaData = await _repository
            .EventRepository
            .GetAllEventsAsync(eventParams, trackChanges);

         var eventsDto = _mapper
            .Map<IEnumerable<EventDto>>(eventWithMetaData);

         var shapedData = _dataShaper
            .EventDataShaper.ShapeData(eventsDto, eventParams.Fields);

         return (events: shapedData, metaData: eventWithMetaData.MetaData);
      }



   }
}
