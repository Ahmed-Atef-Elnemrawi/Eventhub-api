using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.EventRepositories
{
   internal sealed class MediumRepository : BaseRepository<Medium>, IMediumRepository
   {
      public MediumRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public async Task<IEnumerable<Medium>?> GetAllMediumsAsync(bool trackChanges)
      {
         return
             await FindAll(trackChanges)
             .OrderBy(m => m.Type)
             .ToListAsync();
      }

      public async Task<Medium?> GetMediumAsync(Guid mediumId, bool trackChanges)
      {
         return
            await FindByCondition(m => m.MediumId.Equals(mediumId),
            trackChanges)
            .SingleOrDefaultAsync();
      }
   }
}
