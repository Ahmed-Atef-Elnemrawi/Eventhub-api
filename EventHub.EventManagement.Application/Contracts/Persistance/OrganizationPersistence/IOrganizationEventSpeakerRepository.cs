using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence
{
   public interface IOrganizationEventSpeakersRepository
   {

      void CreateEventSpeaker
         (OrganizationEventSpeaker organizationEventSpeaker);

      void CreateEventSpeakers
         (List<OrganizationEventSpeaker> eventSpeakers);

      void RemoveEventSpeaker
         (OrganizationEventSpeaker organizationEventSpeaker);
   }
}
