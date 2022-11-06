using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;

namespace EventHub.EventManagement.Application.Contracts.Service.EventServices
{
   public interface IEventService
   {
      Task<(LinkResponse linkResponse, MetaData metaData)>
         GetAllCategoryEventsAsync(Guid mediumId, Guid categoryId, EventLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetCategoryEventAsync(Guid mediumId, Guid categoryId, Guid eventId, EventLinkParams linkParams, bool trackChanges);

      Task<EventDto?> GetEventAsync(Guid eventId, bool trackChanges);

      Task<(IEnumerable<ShapedEntity> events, MetaData metaData)>
         GetAllEventsAsync(EventParams eventParams, bool trackChanges);

   }
}
