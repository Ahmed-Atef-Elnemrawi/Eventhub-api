using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.EventRepositories
{
   public sealed class EventRepository : BaseRepository<Event>, IEventRepository
   {
      public EventRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public async Task<PagedListTypeEvent<ProducerEvent>> GetCategoryProducerEventsAsync(Guid CategoryId,
         EventParams eventParams, bool trackChanges)
      {
         var events =
              await FindByCondition(e => e.CategoryId.Equals(CategoryId),
              trackChanges)
              .OfType<ProducerEvent>()
              .OrderBy(e => e.Name)
              .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
              .Take(eventParams.PageSize)
              .ToListAsync();

         var count = await FindByCondition(e => e.CategoryId.Equals(CategoryId), trackChanges).CountAsync();
         var upComingCount = await FindByCondition(e =>
            e.EventState.Equals(EventState.Upcoming), false).CountAsync();

         return new PagedListTypeEvent<ProducerEvent>
            (events, count, upComingCount, eventParams.PageNumber, eventParams.PageSize);


      }


      public int GetEventsCount()
      {
         return
             FindAll(false)
            .Count();
      }

      public async Task<PagedListTypeEvent<ProducerEvent>> GetAllproducersEventsAsync
         (EventParams eventParams, bool trackChanges)
      {
         var events =
            await FindAll(trackChanges)
            .OfType<ProducerEvent>()
            .Include(e => e.Producer)
            .Include(e => e.Category)
            .Filter(eventParams)
            .FilterByDate(eventParams)
            .Search(eventParams.SearchTerm)
            .Sort(eventParams.SortBy)
            .OrderBy(e => e.Name)
            .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
            .Take(eventParams.PageSize)
            .OfType<ProducerEvent>()
            .ToListAsync();

         var count = await FindAll(trackChanges).CountAsync();
         var upComingCount = await FindByCondition(e =>
            e.EventState.Equals(EventState.Upcoming), false).CountAsync();

         return new PagedListTypeEvent<ProducerEvent>
            (events, count, upComingCount, eventParams.PageNumber, eventParams.PageSize);

      }

      //public async Task<ProducerEvent?> GetCategoryProducerEventAsync
      //   (Guid categoryId, Guid eventId, bool trackChanges)
      //{
      //   return await FindByCondition(e =>
      //      e.CategoryId.Equals(categoryId) &&
      //      e.EventId.Equals(eventId) &&
      //      e.Discriminator!.Equals("ProducerEvent"), trackChanges).Include(e => e.Producer)
      //      .SingleOrDefaultAsync();
      //}

      //public async Task<OrganizationEvent?> GetCategoryOrganizationEventAsync
      //   (Guid categoryId, Guid eventId, bool trackChanges)
      //{
      //   return await FindByCondition(e =>
      //      e.CategoryId.Equals(categoryId) &&
      //      e.EventId.Equals(eventId) &&
      //      e.Discriminator!.Equals("OrganizationEvent"), trackChanges).SingleOrDefaultAsync() as OrganizationEvent;
      //}
   }



}
