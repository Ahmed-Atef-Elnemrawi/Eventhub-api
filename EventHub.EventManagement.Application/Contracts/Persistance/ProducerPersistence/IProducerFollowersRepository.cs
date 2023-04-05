using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence
{
   public interface IProducerFollowersRepository
   {
      Task<PagedList<Follower>> GetAllFollowersAsync(Guid producerId, FollowerParams followerParameters, bool trackChanges);
      Task<Follower?> GetFollowerAsync(Guid producerId, Guid followerId, bool trackChanges);
      Task<ProducerFollower?> GetProducerFollowerAsync(Guid producerId, Guid followerId);
      void CreateFollower(Guid producerId, Follower follower);
      void RemoveProducerFollower(ProducerFollower producdrFollower);
      void RemoveFollower(Follower follower);
      Task<int> GetProducerFollowersCountAsync(Guid producerId, bool trackChanges);
   }
}
