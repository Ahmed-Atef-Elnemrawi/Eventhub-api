using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence
{
   public interface IOrganizationEventsRepository
   {
      Task<PagedList<OrganizationEvent>> GetAllOrganizationEventsAsync(Guid OrganizationId, EventParams eventParameters, bool trackChanges);
      Task<OrganizationEvent?> GetOrganizationEventAsync(Guid OrganizationId, Guid eventId, bool trackChanges);
      void CreateOrganizationEvent(Guid OrganizationId, OrganizationEvent OrganizationEvent);
      void UpdateOrganizationEvent(OrganizationEvent OrganizationEvent);
      void RemoveOrganizationEvent(OrganizationEvent OrganizationEvent);
   }
}
