using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.OrganizationRepositories
{
   internal sealed class OrganizationFollowersRepository : BaseRepository<Follower>, IOrganizationFollowersRepsoitory
   {
      public OrganizationFollowersRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateFollower(Guid organizationId, Follower follower)
      {
         Create(follower);
         _dbContext.OrganizationsFollowers?.Add(new OrganizationFollower
         {
            OrganizationId = organizationId,
            FollowerId = follower.FollowerId
         });

      }

      public async Task<Follower?> GetFollowerAsync
         (Guid organizationId, Guid followerId, bool trackChanges)
      {
         return await
            FindByCondition(f => f.Organizations
            .Any(o => o.OrganizationId.Equals(organizationId)) &&
            f.FollowerId.Equals(followerId),
            trackChanges)
            .SingleOrDefaultAsync();

      }

      public async Task<PagedList<Follower>> GetAllFollowersAsync(Guid organizationId,
         FollowerParams followerParams, bool trackChanges)
      {
         var followers =
            await FindByCondition(f => f.Organizations
            .Any(o => o.OrganizationId == organizationId), trackChanges)
            .Search(followerParams.SearchTerm)
            .Sort(followerParams.SortBy)
            .Skip((followerParams.PageNumber - 1) * followerParams.PageSize)
            .Take(followerParams.PageSize)
            .ToListAsync();

         var count =
            await FindByCondition(f => f.Organizations
            .Any(o => o.OrganizationId == organizationId), trackChanges)
            .CountAsync();

         return new PagedList<Follower>
            (followers, count, followerParams.PageNumber, followerParams.PageSize);
      }


      public void RemoveFollower(Follower follower)
      {
         Delete(follower);
      }
   }
}
