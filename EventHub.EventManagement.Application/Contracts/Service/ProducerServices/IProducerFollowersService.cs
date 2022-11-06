using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.ProducerServices
{
   public interface IProducerFollowersService
   {
      Task<(LinkResponse linkResponse, MetaData metaData)>
         GetAllFollowersAsync(Guid producerId, FollowerLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetFollowerAsync
         (Guid producerId, Guid followerId, FollowerLinkParams linkParams, bool trackChanges);

      Task<FollowerDto>
         CreateFollowerAsync(Guid producerId, FollowerForCreationDto follower, bool trackChanges);

      Task RemoveFollowerAsync(Guid producerId, Guid followerId, bool trackChanges);

   }
}
