using EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.ProducerRepositories
{
   public sealed class ProducerFollowersRepository : BaseRepository<Follower>, IProducerFollowersRepository
   {
      public ProducerFollowersRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateFollower(Guid producerId, Follower follower)
      {
         var result = FindByCondition(f => f.FollowerId.Equals(follower.FollowerId), false).SingleOrDefault();

         if (result is null)
            Create(follower);

         _dbContext.ProducersFollowers?.Add(
            new ProducerFollower
            {
               ProducerId = producerId,
               FollowerId = follower.FollowerId
            });
      }

      public async Task<ProducerFollower?> GetProducerFollowerAsync(Guid producerId, Guid followerId)
      {
         return await _dbContext.ProducersFollowers!.Where(
             a => a.ProducerId.Equals(producerId)
             && a.FollowerId.Equals(followerId)).SingleOrDefaultAsync();
      }

      public async Task<Follower?> GetFollowerAsync(Guid producerId, Guid followerId, bool trackChanges) =>
         await FindByCondition(f => f.Producers
         .Any(p => p.ProducerId.Equals(producerId)) &&
         f.FollowerId!.Equals(followerId),
         trackChanges)
         .SingleOrDefaultAsync();

      public async Task<PagedList<Follower>> GetAllFollowersAsync(Guid producerId,
         FollowerParams followerParams, bool trackChanges)
      {
         var followers =
            await FindByCondition(f => f.Producers
            .Any(p => p.ProducerId.Equals(producerId)), trackChanges)
            .Search(followerParams.SearchTerm)
            .Sort(followerParams.SortBy)
            .Skip((followerParams.PageNumber - 1) * followerParams.PageSize)
            .Take(followerParams.PageSize)
            .ToListAsync();

         var count =
            await FindByCondition(f => f.Producers
            .Any(p => p.ProducerId == producerId), trackChanges).CountAsync();

         return new PagedList<Follower>
            (followers, count, followerParams.PageNumber, followerParams.PageSize);
      }

      public void RemoveFollower(Follower follower)
      {
         Delete(follower);
      }

      public void RemoveProducerFollower(ProducerFollower producerFollower) =>
         _dbContext.ProducersFollowers?.Remove(producerFollower);


      public async Task<int> GetProducerFollowersCountAsync(Guid producerId, bool trackChanges) =>
            await FindByCondition(f => f.Producers
            .Any(p => p.ProducerId == producerId), trackChanges).CountAsync();
   }
}
