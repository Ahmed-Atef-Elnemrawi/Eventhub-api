using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationEventSpeakerService : IOrganizationEventSpeakerService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationEventSpeakerService
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



      public async Task<LinkResponse> GetAllEventSpeakersAsync
         (Guid organizationId, Guid eventId, SpeakerLinkParams linkParam, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var speakers = await _repository
            .SpeakerRepositoy
            .GetAllSpeakersByEventAsync(eventId, trackChanges);

         var speakersDto = _mapper
            .Map<IEnumerable<SpeakerDto>>(speakers);

         var linkResponse = _entitiesLinkGenerator.OrganizationEventSpeakerLinks
            .TryGetEntitiesLinks(speakersDto,
                                 linkParam.speakerParams.Fields!,
                                 linkParam.HttpContext,
                                 eventId,
                                 organizationId);

         return linkResponse;
      }


      public async Task<LinkResponse> GetEventSpeaker
        (Guid organizationId, Guid eventId, Guid speakerId, SpeakerLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);

         var speaker = await _repository
            .SpeakerRepositoy
            .GetSpeakerByEventAsync(eventId, speakerId, trackChanges);

         if (speaker is null)
            throw new SpeakerNotFound("id", speakerId);

         var speakerDto = _mapper
            .Map<SpeakerDto>(speaker);

         var linkResponse = _entitiesLinkGenerator.OrganizationEventSpeakerLinks
            .TryGetEntityLinks(speakerDto,
                               linkParams.speakerParams.Fields!,
                               linkParams.HttpContext,
                               eventId,
                               organizationId);

         return linkResponse;
      }

      public async Task AddEventSpeakerAsync(Guid organizationId,
                                             Guid eventId,
                                             EventSpeakerForCreationDto eventSpeakerForCreationDto,
                                             bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);
         await CheckIfOrganizationSpeakerExists
            (organizationId, eventSpeakerForCreationDto.SpeakerId, trackChanges);

         var eventSpeaker = new OrganizationEventSpeaker
         {
            OrganizationEventId = eventId,
            SpeakerId = eventSpeakerForCreationDto.SpeakerId
         };

         _repository.OrganizationEventSpeakersRepository
            .CreateEventSpeaker(eventSpeaker);

         await _repository.SaveAsync();
      }


      public async Task RemoveEventSpeakerAsync
         (Guid organizationId, Guid eventId, Guid speakerId, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);
         await CheckIfOrganizationEventExists(organizationId, eventId, trackChanges);
         await CheckIfEventSpeakerExists(eventId, speakerId, trackChanges);

         var eventSpeaker = new OrganizationEventSpeaker
         {
            OrganizationEventId = eventId,
            SpeakerId = speakerId
         };

         _repository.OrganizationEventSpeakersRepository
            .RemoveEventSpeaker(eventSpeaker);

         await _repository.SaveAsync();

      }


      private async Task CheckIfOrganizationExists
         (Guid organizationId, bool trackChanges)
      {
         var organization = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges: false);

         if (organization is null)
            throw new OrganizationNotFound("id", organizationId);

      }

      private async Task CheckIfOrganizationEventExists
         (Guid organizationId, Guid eventId, bool trackChanges)
      {
         var organizationEvent = await _repository
            .OrganizationEventsRepository
            .GetOrganizationEventAsync(organizationId, eventId, trackChanges: false);

         if (organizationEvent is null)
            throw new EventNotFound("id", eventId);
      }

      private async Task CheckIfEventSpeakerExists
         (Guid eventId, Guid speakerId, bool trackChanges)
      {
         var speaker = await _repository.SpeakerRepositoy
            .GetSpeakerByEventAsync(eventId, speakerId, trackChanges);

         if (speaker is null)
            throw new SpeakerNotFound("id", speakerId);

      }

      private async Task CheckIfOrganizationSpeakerExists
         (Guid organizationId, Guid speakerId, bool trackChanges)
      {
         var speaker = await _repository.SpeakerRepositoy.
            GetSpeakerByOrganizationAsync(organizationId, speakerId, trackChanges);

         if (speaker is null)
            throw new SpeakerNotFound("id", speakerId);

      }


   }
}
