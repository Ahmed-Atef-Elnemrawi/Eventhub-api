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
   public sealed class OrganizationEventRepository : BaseRepository<OrganizationEvent>, IOrganizationEventsRepository
   {
      public OrganizationEventRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateOrganizationEvent
         (Guid organizationId, OrganizationEvent organizationEvent)
      {
         organizationEvent.OrganizationId = organizationId;
         Create(organizationEvent);

         var o = organizationEvent.EventId;
      }

      public async Task<PagedList<OrganizationEvent>> GetAllOrganizationEventsAsync
         (Guid organizationId, EventParams eventParameters, bool trackChanges)
      {
         var events = await FindByCondition(e => e.OrganizationId.Equals(organizationId),
            trackChanges)
            .Sort(eventParameters.SortBy)
            .Search(eventParameters.SearchTerm)
            .Skip((eventParameters.PageNumber - 1) * eventParameters.PageSize)
            .Take(eventParameters.PageSize)
            .ToListAsync();

         var count =
            await FindByCondition(e => e.OrganizationId.Equals(organizationId), trackChanges).CountAsync();

         return new PagedList<OrganizationEvent>
            (events, count, eventParameters.PageNumber, eventParameters.PageSize);
      }

      public async Task<OrganizationEvent?> GetOrganizationEventAsync
         (Guid OrganizationId, Guid eventId, bool trackChanges)
      {
         return await FindByCondition(e => e.OrganizationId.Equals(OrganizationId)
                && e.EventId.Equals(eventId),
         trackChanges).SingleOrDefaultAsync();
      }

      public void RemoveOrganizationEvent(OrganizationEvent OrganizationEvent) =>
         Delete(OrganizationEvent);

      public void UpdateOrganizationEvent(OrganizationEvent organizationEvent)
      {
         Update(organizationEvent);
      }
   }
}
