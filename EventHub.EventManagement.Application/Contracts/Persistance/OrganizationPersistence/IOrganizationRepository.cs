using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence
{
   public interface IOrganizationRepository
   {
      Task<PagedList<Organization>> GetAllOrganizationsAsync(OrganizationParams organizationParams, bool trackChanges);
      Task<Organization?> GetOrganizationAsync(Guid id, bool trackChanges);
      void CreateOrganization(Organization Organization);
      void RemoveOrganization(Organization Organization);
      void UpdateOrganization(Organization Organization);
   }
}
