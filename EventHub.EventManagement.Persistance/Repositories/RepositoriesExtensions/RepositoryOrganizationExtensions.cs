using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions
{
   internal static class RepositoryOrganizationExtensions
   {
      public static IQueryable<Organization> Filter
         (this IQueryable<Organization> collection, OrganizationParams organizationParams)
      {
         if (!string.IsNullOrEmpty(organizationParams.country))
            collection = collection.Where(o => o.Country!.Equals(organizationParams.country));

         if (!string.IsNullOrEmpty(organizationParams.city))
            collection = collection.Where(o => o.City!.Equals(organizationParams.city));

         if (!string.IsNullOrEmpty(organizationParams.BusinessType))
            collection = collection.Where(o => o.BusinessType == organizationParams.BusinessType);

         return collection;
      }
   }
}
