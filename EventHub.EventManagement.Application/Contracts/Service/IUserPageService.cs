using EventHub.EventManagement.Application.DTOs;

namespace EventHub.EventManagement.Application.Contracts.Service
{
   public interface IUserPageService
   {
      Task<UserPageDto> GetUserPageAsync(Guid userPageId, bool trackChanges);
   }
}
