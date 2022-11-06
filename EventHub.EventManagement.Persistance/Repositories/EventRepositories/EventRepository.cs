using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.EventRepositories
{
   internal sealed class EventRepository : BaseRepository<Event>, IEventRepository
   {
      public EventRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public async Task<PagedList<Event>> GetCategoryEventsAsync(Guid CategoryId,
         EventParams eventParams, bool trackChanges)
      {
         var events =
              await FindByCondition(e => e.CategoryId.Equals(CategoryId),
              trackChanges)
              .OrderBy(e => e.Name)
              .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
              .Take(eventParams.PageSize)
              .ToListAsync();

         var count = await FindByCondition(e => e.CategoryId.Equals(CategoryId), trackChanges).CountAsync();

         return new PagedList<Event>
            (events, count, eventParams.PageNumber, eventParams.PageSize);


      }


      public async Task<Event?> GetCategoryEventAsync(Guid categoryId, Guid eventId, bool trackChanges)
      {
         return
            await FindByCondition(e => e.EventId.Equals(eventId)
            && e.CategoryId.Equals(categoryId),
            trackChanges)
            .SingleOrDefaultAsync();
      }

      public int GetEventsCount()
      {
         return
             FindAll(false)
            .Count();
      }

      public async Task<Event?> GetEventAsync(Guid eventId, bool trackChanges)
      {
         return
            await FindByCondition(e =>
            e.EventId.Equals(eventId),
            trackChanges)
            .SingleOrDefaultAsync();
      }

      public async Task<PagedList<Event>> GetAllEventsAsync(EventParams eventParams, bool trackChanges)
      {
         var events =
            await FindAll(trackChanges)
            .Filter(eventParams)
            .FilterByDate(eventParams)
            .Search(eventParams.SearchTerm)
            .Sort(eventParams.SortBy)
            .OrderBy(e => e.Name)
            .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
            .Take(eventParams.PageSize)
            .ToListAsync();

         var count = await FindAll(trackChanges).CountAsync();

         return new PagedList<Event>
            (events, count, eventParams.PageNumber, eventParams.PageSize);

      }

      public async Task<ProducerEvent?> GetCategoryProducerEventAsync
         (Guid categoryId, Guid eventId, bool trackChanges)
      {
         return await FindByCondition(e =>
            e.CategoryId.Equals(categoryId) &&
            e.EventId.Equals(eventId) &&
            e.Discriminator!.Equals("ProducerEvent"), trackChanges).SingleOrDefaultAsync() as ProducerEvent;
      }

      public async Task<OrganizationEvent?> GetCategoryOrganizationEventAsync
         (Guid categoryId, Guid eventId, bool trackChanges)
      {
         return await FindByCondition(e =>
            e.CategoryId.Equals(categoryId) &&
            e.EventId.Equals(eventId) &&
            e.Discriminator!.Equals("OrganizationEvent"), trackChanges).SingleOrDefaultAsync() as OrganizationEvent;
      }
   }



}
