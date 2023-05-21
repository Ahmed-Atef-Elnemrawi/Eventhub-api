using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.EventServices
{
   public interface IEventService
   {
      Task<(LinkResponse linkresponse, MetaDataTypeEvent metaData)>
         GetAllCategoryProducerEventsAsync(Guid mediumId, Guid categoryId, EventLinkParams linkParams, bool trackChanges);

      //Task<LinkResponse> GetCategoryEventAsync(Guid mediumId, Guid categoryId, Guid eventId, EventLinkParams linkParams, bool trackChanges);

      //Task<ShapedEntity> GetEventAsync(Guid eventId, string fields, bool trackChanges);

      Task<(LinkResponse linkResponse, MetaDataTypeEvent metaData)>
         GetAllProducersEventsAsync(EventLinkParams linkParams, bool trackChanges);

   }
}
