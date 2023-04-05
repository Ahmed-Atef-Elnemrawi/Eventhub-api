using EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.ProducerRepositories
{
   internal sealed class ProducerEventRepositroy : BaseRepository<ProducerEvent>, IProducerEventsRepository
   {
      public ProducerEventRepositroy(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateProducerEvent(Guid producerId, ProducerEvent producerEvent)
      {
         producerEvent.ProducerId = producerId;
         Create(producerEvent);
      }

      public async Task<PagedListTypeEvent<ProducerEvent>> GetAllProducerEventsAsync
         (Guid producerId, EventParams eventParams, bool trackChanges)
      {
         var events =
             await FindByCondition(e => e.ProducerId.Equals(producerId),
             trackChanges)
            .Search(eventParams.SearchTerm)
            .Sort(eventParams.SortBy)
            .FilterByDate(eventParams)
            .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
            .Take(eventParams.PageSize)
            .ToListAsync();

         var count =
            await FindByCondition(e => e.ProducerId.Equals(producerId), trackChanges).CountAsync();

         var upcomingCount = await FindByCondition(e => e.ProducerId.Equals(producerId)
         && e.Date > DateTime.Now, trackChanges)
         .CountAsync();

         return new PagedListTypeEvent<ProducerEvent>
            (events, count, upcomingCount, eventParams.PageNumber, eventParams.PageSize);
      }

      public async Task<ProducerEvent?> GetProducerEventAsync
         (Guid producerId, Guid eventId, bool trackChanges)
      {
         return await FindByCondition(e => e.ProducerId.Equals(producerId)
         && e.EventId.Equals(eventId),
         trackChanges).SingleOrDefaultAsync();
      }

      public void RemoveProducerEvent(ProducerEvent producerEvent) =>
         Delete(producerEvent);

      public void UpdateProducerEvent(ProducerEvent producerEvent) =>
         Update(producerEvent);


      public async Task<PagedList<ProducerEvent>> GetAllEventsAsync
         (Guid attendantId, EventParams eventParams, bool trackChanges)
      {
         var events = await FindByCondition(e =>
         e.Attendants.Any(a => a.AttendantId.Equals(attendantId))
         , trackChanges)
            .Filter(eventParams)
            .FilterByDate(eventParams)
            .Search(eventParams.SearchTerm)
            .Sort(eventParams.SortBy)
            .OrderBy(e => e.Name)
            .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
            .Take(eventParams.PageSize)
            .OfType<ProducerEvent>()
            .Include(e => e.Producer)
            .Include(e => e.Category)
            .ToListAsync();

         var count = await FindByCondition(e =>
         e.Attendants.Any(a => attendantId.Equals(attendantId)), trackChanges).CountAsync();

         return new PagedList<ProducerEvent>(events, count, eventParams.PageNumber, eventParams.PageSize);
      }

      public async Task<List<DateOnly>> GetDistictAttendantEventsDateAsync(Guid attendantId, bool trackChanges)
      {
         return await FindByCondition(e => e.Attendants.Any(
             a => a.AttendantId.Equals(attendantId)), trackChanges)
             .Select(e => new DateOnly(e.Date.Year, e.Date.Month, 1))
             .Distinct().ToListAsync();


      }

      public async Task<List<ProducerEvent>> GetUpcomingProducerEvents(Guid producerId, bool trackChanges)
      {
         return await FindByCondition(e => e.ProducerId.Equals(producerId)
         && e.Date.Millisecond > DateTime.Now.Millisecond, trackChanges)
         .ToListAsync();
      }

   }
}
