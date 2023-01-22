using EventHub.EventManagement.Application.DTOs.SpeakerDtos;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationEventSpeakerService
   {
      Task<LinkResponse> GetAllEventSpeakersAsync
        (Guid organizationId, Guid eventId, SpeakerLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetEventSpeaker
        (Guid organizationId, Guid eventId, Guid speakerId, SpeakerLinkParams linkParams, bool trackChanges);

      Task AddEventSpeakerAsync
         (Guid organizationId, Guid eventId, EventSpeakerForCreationDto eventSpeakerForCreationDto, bool trackChanges);

      Task RemoveEventSpeakerAsync
         (Guid organizationId, Guid eventId, Guid speakerId, bool trackChanges);

   }
}
