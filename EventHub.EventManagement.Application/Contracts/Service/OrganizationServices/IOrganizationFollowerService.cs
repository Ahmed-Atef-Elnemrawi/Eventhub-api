using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationFollowersService
   {
      Task<(LinkResponse link, MetaData metaData)>
         GetAllFollowersAsync(Guid organizationId, FollowerLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetFollowerAsync
         (Guid organizationId, Guid followerId, FollowerLinkParams linkParams, bool trackChanges);

      Task<FollowerDto> CreateFollowerAsync
         (Guid organizationId, FollowerForCreationDto follower, bool trackChanges);

      Task RemoveFollowerAsync(Guid organizationId, Guid followerId, bool trackChanges);

   }
}
