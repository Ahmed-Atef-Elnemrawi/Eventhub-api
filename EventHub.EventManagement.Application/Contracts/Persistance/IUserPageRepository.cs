using EventHub.EventManagement.Domain.Entities;


namespace EventHub.EventManagement.Application.Contracts.Persistance
{
   public interface IUserPageRepository
   {
      Task<UserPage?> GetUserPageAsync(Guid UserPageId, bool trackChanges);
   }
}
