using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.UserPagePersistence
{
   public interface IUserPageRepository
   {
      Task<PagedList<UserPage>> GetAllUsersPagesAsync(UserPageParams pagesParams, bool trackChanges);
      Task<UserPage?> GetUserPageAsync(Guid id, bool trackChanges);
      void CreateUserPage(UserPage userPage);
      void UpdateUserPage(UserPage userPage);
      void RemoveUserPage(UserPage userPage);

   }
}
