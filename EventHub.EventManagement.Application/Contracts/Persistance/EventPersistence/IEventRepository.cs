using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence
{
   public interface IEventRepository
   {
      Task<ProducerEvent?> GetCategoryProducerEventAsync(Guid categoryId, Guid eventId, bool trackChanges);
      Task<OrganizationEvent?> GetCategoryOrganizationEventAsync(Guid categoryId, Guid eventId, bool trackChanges);
      Task<Event?> GetEventAsync(Guid eventId, bool trackChanges);
      Task<PagedList<Event>> GetCategoryEventsAsync(Guid CategoryId, EventParams eventParams, bool trackChanges);
      Task<PagedList<Event>> GetAllEventsAsync(EventParams eventParams, bool trackChanges);
      int GetEventsCount();
   }
}
