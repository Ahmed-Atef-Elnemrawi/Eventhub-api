using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;

namespace EventHub.EventManagement.Persistance.Repositories.OrganizationRepositories
{
   public sealed class OrganizationEventSpeakerRepository
      : BaseRepository<OrganizationEventSpeaker>, IOrganizationEventSpeakersRepository
   {
      public OrganizationEventSpeakerRepository
         (RepositoryContext dbContext) : base(dbContext)
      {

      }

      public void CreateEventSpeaker(OrganizationEventSpeaker organizationEventSpeaker) =>
         Create(organizationEventSpeaker);

      public void CreateEventSpeakers(List<OrganizationEventSpeaker> eventSpeakers) =>
         _dbContext.OrganizationsEventSpeakers!.AddRange(eventSpeakers);

      public void RemoveEventSpeaker(OrganizationEventSpeaker organizationEventSpeaker) =>
         Delete(organizationEventSpeaker);
   }
}
