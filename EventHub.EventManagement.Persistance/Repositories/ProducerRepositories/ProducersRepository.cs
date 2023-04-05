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
   internal sealed class ProducersRepository : BaseRepository<Producer>, IProducerRepository
   {
      public ProducersRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateProducer(Producer producer) => Create(producer);

      public async Task<Producer?> GetProducerAsync(Guid id, bool trackChanges) =>
         await FindByCondition(p => p.ProducerId.Equals(id),
            trackChanges)
            .SingleOrDefaultAsync();

      public async Task<PagedList<Producer>> GetAllProducersAsync
         (ProducerParams producderParams, bool trackChanges)
      {
         var producers =
            await FindAll(trackChanges)
            .Filter(producderParams)
            .Search(producderParams.SearchTerm)
            .Sort(producderParams.SortBy)
            .Skip((producderParams.PageNumber - 1) * producderParams.PageSize)
            .Take(producderParams.PageSize)
            .ToListAsync();

         int count = await FindAll(trackChanges).CountAsync();

         return new PagedList<Producer>
            (producers, count, producderParams.PageNumber, producderParams.PageSize);
      }

      public void UpdateProducer(Producer producer) =>
         Update(producer);

      public void RemoveProducer(Producer producer) =>
         Delete(producer);

      public async Task<List<Producer>> GetAllProducersAsync(Guid followerId, bool trackChanges)
      {
         return await FindByCondition(p => p.Followers.Any(f => f.FollowerId.Equals(followerId)),
            trackChanges).ToListAsync();
      }

      public async Task<Producer?> GetProducerAsync(Guid followerId, Guid producerId, bool trackChanges)
      {
         return await FindByCondition(p => p.Followers.Any(f => f.FollowerId.Equals(followerId)),
            trackChanges).SingleOrDefaultAsync(p => p.ProducerId.Equals(producerId));
      }

   }
}
