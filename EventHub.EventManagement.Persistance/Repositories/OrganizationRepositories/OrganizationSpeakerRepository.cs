using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.OrganizationRepositories
{
   public sealed class OrganizationSpeakerRepository : BaseRepository<Speaker>, ISpeakerRepository
   {
      public OrganizationSpeakerRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateOrganizationSpeaker(Guid organizationId, Speaker speaker)
      {
         speaker.OrganizationId = organizationId;
         Create(speaker);
      }

      public async Task<Speaker?> GetSpeakerByOrganizationAsync
         (Guid organizationId, Guid speakerId, bool trackChanges)
      {
         return
             await FindByCondition(s =>
             s.OrganizationId.Equals(organizationId) &&
             s.SpeakerId.Equals(speakerId),
             trackChanges)
               .SingleOrDefaultAsync();
      }

      public async Task<Speaker?> GetSpeakerByEventAsync(Guid eventId, Guid speakerId, bool trackChanges)
      {
         return
            await FindByCondition(s =>
               s.SpeakerId.Equals(speakerId) &&
               s.OrganizationEvents.Any(e => e.EventId.Equals(eventId)),
               trackChanges).SingleOrDefaultAsync();
      }

      public async Task<IEnumerable<Speaker>> GetAllSpeakersByOrganizationAsync
         (Guid organizationId, bool trackChanges)
      {
         var m =
            await FindByCondition(s =>
            s.OrganizationId.Equals(organizationId),
            trackChanges)
            .ToListAsync();

         return m;
      }

      public async Task<IEnumerable<Speaker>> GetAllSpeakersByEventAsync
         (Guid eventId, bool trackChanges)
      {
         return
            await FindByCondition(s =>
               s.OrganizationEvents.Any(e => e.EventId.Equals(eventId)),
               trackChanges).ToListAsync();
      }

      public void RemoveOrganizationSpeaker(Speaker speaker)
      {
         Delete(speaker);
      }

      public void UpdateOrganizationSpeaker(Speaker speaker)
      {
         Update(speaker);
      }
   }
}
