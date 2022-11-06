using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence
{
   public interface IProducerFollowersRepository
   {
      Task<PagedList<Follower>> GetAllFollowersAsync(Guid producerId, FollowerParams followerParameters, bool trackChanges);
      Task<Follower?> GetFollowerAsync(Guid producerId, Guid followerId, bool trackChanges);
      void CreateFollower(Guid producerId, Follower follower);
      void RemoveFollower(Follower follower);
   }
}
