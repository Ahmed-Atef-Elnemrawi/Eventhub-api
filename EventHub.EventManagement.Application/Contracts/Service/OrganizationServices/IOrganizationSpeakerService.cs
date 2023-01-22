using EventHub.EventManagement.Application.DTOs.SpeakerDtos;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationSpeakerService
   {
      Task<LinkResponse> GetAllOrganizationSpeakers
         (Guid organizationId, SpeakerLinkParams linkParam, bool trackChanges);

      Task<LinkResponse> GetOrganizationSpeaker
         (Guid organizationId, Guid speakerId, SpeakerLinkParams linkParams, bool trackChanges);

      Task<SpeakerDto> CreateOrganizationSpeakerAsync
         (Guid organizationId, SpeakerForCreationDto speakerForCreationDto, bool trackChanges);

      Task UpdateOrganizationSpeakerAsync
         (Guid organizationId, Guid speakerId, SpeakerForUpdateDto speakerForUpdateDto, bool trackChanges);

      Task RemoveOrganizationSpeakerAsync
         (Guid organizationId, Guid speakerId, bool trackChanges);
   }
}
