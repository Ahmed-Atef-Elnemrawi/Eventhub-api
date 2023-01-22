using EventHub.EventManagement.Application.Contracts.Persistance.UserPagePersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.UserEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories
{
   internal sealed class UserPageRepository : BaseRepository<UserPage>, IUserPageRepository
   {
      public UserPageRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateUserPage(UserPage userPage)
      {
         Create(userPage);
      }

      public async Task<PagedList<UserPage>> GetAllUsersPagesAsync(UserPageParams pagesParams, bool trackChanges)
      {
         var usersPages = await FindAll(trackChanges)
            .Search(pagesParams.SearchTerm)
            .Sort(pagesParams.SortBy)
            .Skip((pagesParams.PageNumber - 1) * pagesParams.PageSize)
            .Take(pagesParams.PageSize)
            .ToListAsync();

         var count = await FindAll(trackChanges).CountAsync();

         return new PagedList<UserPage>(usersPages, count, pagesParams.PageNumber, pagesParams.PageSize);
      }

      public async Task<UserPage?> GetUserPageAsync(Guid id, bool trackChanges)
      {
         return await FindByCondition(p => p.UserPageId.Equals(id), trackChanges).SingleOrDefaultAsync();
      }

      public void RemoveUserPage(UserPage userPage)
      {
         throw new NotImplementedException();
      }

      public void UpdateUserPage(UserPage userPage)
      {
         throw new NotImplementedException();
      }
   }
}
