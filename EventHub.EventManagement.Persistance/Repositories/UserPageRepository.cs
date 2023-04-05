using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories
{
   public sealed class UserPageRepository : BaseRepository<UserPage>, IUserPageRepository
   {
      public UserPageRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public async Task<UserPage?> GetUserPageAsync(Guid UserPageId, bool trackChanges)
      {
         return await FindByCondition(p => p.UserPageId.Equals(UserPageId), trackChanges).SingleOrDefaultAsync();
      }
   }
}
