using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence
{
   public interface IMediumRepository
   {
      Task<IEnumerable<Medium>?> GetAllMediumsAsync(bool trackChanges);
      Task<Medium?> GetMediumAsync(Guid mediumId, bool trackChanges);
   }
}
