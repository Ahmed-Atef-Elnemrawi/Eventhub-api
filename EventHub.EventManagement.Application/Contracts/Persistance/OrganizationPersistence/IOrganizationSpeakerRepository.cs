using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence
{
   public interface ISpeakerRepository
   {
      Task<IEnumerable<Speaker>> GetAllSpeakersByOrganizationAsync(Guid organizationId, bool trackChanges);
      Task<IEnumerable<Speaker>> GetAllSpeakersByEventAsync(Guid eventId, bool trackChanges);
      Task<Speaker?> GetSpeakerByOrganizationAsync(Guid organizationId, Guid speakerId, bool trackChanges);
      Task<Speaker?> GetSpeakerByEventAsync(Guid eventId, Guid speakerId, bool trackChanges);
      void CreateOrganizationSpeaker(Guid organizationId, Speaker speaker);
      void UpdateOrganizationSpeaker(Speaker speaker);
      void RemoveOrganizationSpeaker(Speaker speaker);
   }
}
