using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence
{
   public interface IOrganizationFollowersRepsoitory
   {
      Task<PagedList<Follower>> GetAllFollowersAsync(Guid organizationId, FollowerParams followerParams, bool trackChanges);
      Task<Follower?> GetFollowerAsync(Guid organizationId, Guid followerId, bool trackChanges);
      void CreateFollower(Guid organizationId, Follower follower);
      void RemoveFollower(Follower follower);
   }
}
