using AutoMapper;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Logging;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationEventService : IOrganizationEventService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationEventService
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

      public async Task<EventDto> CreateOrganizationEventAsync
         (Guid organizationId, OrganizationEventForCreationDto eventForCreationDto, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var eventEntity = _mapper
            .Map<OrganizationEvent>(eventForCreationDto);

         _repository
            .OrganizationEventsRepository
            .CreateOrganizationEvent(organizationId, eventEntity);

         var speakersIds = eventForCreationDto.SpeakersId;
         var listOfEventSpeakers = new List<OrganizationEventSpeaker>();

         if (speakersIds is not null)
         {
            foreach (var id in speakersIds)
            {
               var eventSpeaker = new OrganizationEventSpeaker
               {
                  OrganizationEventId = eventEntity.EventId,
                  SpeakerId = id
               };
               listOfEventSpeakers.Add(eventSpeaker);
            }
         }

         _repository.OrganizationEventSpeakersRepository
            .CreateEventSpeakers(listOfEventSpeakers);

         await _repository.SaveAsync();

         var eventToReturn = _mapper
            .Map<EventDto>(eventEntity);

         return eventToReturn;
      }

      public async Task<(LinkResponse link, MetaData metaData)>
         GetAllOrganizationEventsAsync(Guid organizationId, EventLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var eventsWithMetaData = await _repository
            .OrganizationEventsRepository
            .GetAllOrganizationEventsAsync(organizationId, linkParams.eventParams, trackChanges);

         var eventsDto = _mapper
            .Map<IEnumerable<EventDto>>(eventsWithMetaData);

         var link = _entitiesLinkGenerator.OrganizationEventLinks
            .TryGetEntitiesLinks(eventsDto,
                                 linkParams.eventParams.Fields!,
                                 linkParams.HttpContext,
                                 organizationId);

         return (link, metaData: eventsWithMetaData.MetaData);
      }

      public async Task<LinkResponse> GetOrganizationEventAsync
         (Guid organizationId, Guid eventId, EventLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var eventEntity = await
            GetEventAndCheckIfItExists(organizationId, eventId, trackChanges);

         var eventDto =
            _mapper.Map<EventDto>(eventEntity);

         var linkResponse = _entitiesLinkGenerator.OrganizationEventLinks
            .TryGetEntityLinks(eventDto,
                               linkParams.eventParams.Fields!,
                               linkParams.HttpContext,
                               organizationId);
         return linkResponse;
      }

      public async Task RemoveOrganizationEventAsync
         (Guid organizationId, Guid eventId, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var eventEntity = await
            GetEventAndCheckIfItExists(organizationId, eventId, trackChanges);

         if (eventEntity is null)
            throw new EventNotFoundException("id", eventId);

         _repository
            .OrganizationEventsRepository
            .RemoveOrganizationEvent(eventEntity);

         await _repository.SaveAsync();
      }

      public async Task UpdateOrganizationEventAsync(Guid organizationId, Guid eventId,
         EventForUpdateDto eventForUpdate, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, false);

         var eventEntity = await
            GetEventAndCheckIfItExists(organizationId, eventId, trackChanges);

         _mapper.Map(eventForUpdate, eventEntity);

         await _repository.SaveAsync();
      }


      private async Task CheckIfOrganizationExists(Guid organizationId, bool trackChanges)
      {
         var organization = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges: false);

         if (organization is null)
            throw new OrganizationNotFoundException("id", organizationId);
      }

      private async Task<OrganizationEvent> GetEventAndCheckIfItExists(Guid organizationId,
         Guid eventId, bool trackChanges)
      {
         var eventEntity = await _repository
            .OrganizationEventsRepository
            .GetOrganizationEventAsync(organizationId, eventId, trackChanges);

         if (eventEntity is null)
            throw new EventNotFoundException("id", eventId);

         return eventEntity;
      }
   }
}
