using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence
{
   public interface IEventRepository
   {
      //Task<ProducerEvent?> GetCategoryProducerEventAsync(Guid categoryId, Guid eventId, bool trackChanges);
      //Task<OrganizationEvent?> GetCategoryOrganizationEventAsync(Guid categoryId, Guid eventId, bool trackChanges);
      Task<PagedListTypeEvent<ProducerEvent>> GetCategoryProducerEventsAsync(Guid CategoryId, EventParams eventParams, bool trackChanges);
      Task<PagedListTypeEvent<ProducerEvent>> GetAllproducersEventsAsync(EventParams eventParams, bool trackChanges);
      int GetEventsCount();
   }
}
