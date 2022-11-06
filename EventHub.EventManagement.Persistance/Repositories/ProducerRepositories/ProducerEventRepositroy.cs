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

      public async Task<PagedList<ProducerEvent>> GetAllProducerEventsAsync
         (Guid producerId, EventParams eventParams, bool trackChanges)
      {
         var events =
             await FindByCondition(e => e.ProducerId.Equals(producerId),
             trackChanges)
            .Search(eventParams.SearchTerm)
            .Sort(eventParams.SortBy)
            .Skip((eventParams.PageNumber - 1) * eventParams.PageSize)
            .Take(eventParams.PageSize)
            .ToListAsync();

         var count =
            await FindByCondition(e => e.ProducerId.Equals(producerId), trackChanges).CountAsync();

         return new PagedList<ProducerEvent>
            (events, count, eventParams.PageNumber, eventParams.PageSize);
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

   }
}
