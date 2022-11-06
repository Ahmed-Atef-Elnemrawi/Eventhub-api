using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.OrganizationRepositories
{
   internal sealed class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
   {
      public OrganizationRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateOrganization(Organization organization)
      {
         var single = FindByCondition(o => o == organization, trackChanges: false).FirstOrDefault();

         if (single == null)
            Create(organization);
      }

      public async Task<Organization?> GetOrganizationAsync(Guid id, bool trackChanges) =>
         await FindByCondition(o => o.OrganizationId.Equals(id),
            trackChanges)
            .SingleOrDefaultAsync();

      public async Task<PagedList<Organization>> GetAllOrganizationsAsync(
         OrganizationParams organiationParams, bool trackChanges)
      {
         var organizations =
            await FindAll(trackChanges)
            .Search(organiationParams.SearchTerm)
            .Filter(organiationParams)
            .Sort(organiationParams.SortBy)
            .Skip((organiationParams.PageNumber - 1) * organiationParams.PageSize)
            .Take(organiationParams.PageSize)
            .ToListAsync();

         var count = await FindAll(trackChanges).CountAsync();

         return new PagedList<Organization>
            (organizations, count, organiationParams.PageNumber, organiationParams.PageSize);
      }

      public void UpdateOrganization(Organization organization) =>
         Update(organization);

      public void RemoveOrganization(Organization organization) =>
         Delete(organization);


   }
}
