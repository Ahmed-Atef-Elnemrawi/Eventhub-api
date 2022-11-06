using AutoMapper;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Logging;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationSpeakerService : IOrganizationSpeakerService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationSpeakerService
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

      public async Task<SpeakerDto> CreateOrganizationSpeakerAsync
         (Guid organizationId, SpeakerForCreationDto speakerForCreationDto, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var speaker = _mapper
            .Map<Speaker>(speakerForCreationDto);

         _repository
            .SpeakerRepositoy
            .CreateOrganizationSpeaker(organizationId, speaker);

         await _repository.SaveAsync();

         var speakerDto = _mapper.Map<SpeakerDto>(speaker);

         return speakerDto;

      }


      public async Task<LinkResponse> GetAllOrganizationSpeakers
         (Guid organizationId, SpeakerLinkParams linkParam, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var speakers = await _repository
            .SpeakerRepositoy
            .GetAllSpeakersByOrganizationAsync(organizationId, trackChanges);

         var speakersDto = _mapper
            .Map<IEnumerable<SpeakerDto>>(speakers);

         var linkResponse = _entitiesLinkGenerator.OrganizationSpeakerLinks
            .TryGetEntitiesLinks(speakersDto,
                                 linkParam.speakerParams.Fields!,
                                 linkParam.HttpContext,
                                 organizationId);

         return linkResponse;
      }


      public async Task<LinkResponse> GetOrganizationSpeaker
         (Guid organizationId, Guid speakerId, SpeakerLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var speaker =
            await GetSpeakerByOrganizationAndCheckIfItExists(organizationId, speakerId, trackChanges);

         var speakerDto = _mapper
            .Map<SpeakerDto>(speaker);

         var linkResponse = _entitiesLinkGenerator.OrganizationSpeakerLinks
            .TryGetEntityLinks(speakerDto,
                               linkParams.speakerParams.Fields!,
                               linkParams.HttpContext,
                               organizationId);

         return linkResponse;
      }


      public async Task RemoveOrganizationSpeakerAsync
         (Guid organizationId, Guid speakerId, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var speaker =
            await GetSpeakerByOrganizationAndCheckIfItExists(organizationId, speakerId, trackChanges);

         _repository
            .SpeakerRepositoy
            .RemoveOrganizationSpeaker(speaker);

         await _repository.SaveAsync();
      }

      public async Task UpdateOrganizationSpeakerAsync(Guid organizationId, Guid speakerId,
         SpeakerForUpdateDto speakerForUpdateDto, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, false);

         var speaker =
            await GetSpeakerByOrganizationAndCheckIfItExists(organizationId, speakerId, trackChanges);

         _mapper.Map(speakerForUpdateDto, speaker);

         await _repository.SaveAsync();


      }


      private async Task CheckIfOrganizationExists(Guid organizationId, bool trackChanges)
      {
         var organization = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges);

         if (organization is null)
            throw new OrganizationNotFoundException("id", organizationId);
      }


      private async Task<Speaker> GetSpeakerByOrganizationAndCheckIfItExists
         (Guid organizationId, Guid speakerId, bool trackChanges)
      {
         var speaker = await _repository
            .SpeakerRepositoy
            .GetSpeakerByOrganizationAsync(organizationId, speakerId, trackChanges);

         if (speaker is null)
            throw new SpeakerNotFoundException("id", speakerId);

         return speaker;
      }

      private async Task CheckIfEventExists(Guid organizationId, Guid eventId, bool trackChanges)
      {
         var organizationEvent = await _repository
            .OrganizationEventsRepository
            .GetOrganizationEventAsync(organizationId, eventId, trackChanges);

         if (organizationEvent is null)
            throw new EventNotFoundException("id", eventId);
      }

   }
}
