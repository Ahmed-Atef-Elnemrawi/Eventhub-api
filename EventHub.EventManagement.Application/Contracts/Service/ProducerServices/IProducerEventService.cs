using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.ProducerServices
{
   public interface IProducerEventService
   {
      Task<(LinkResponse linkResponse, MetaDataTypeEvent metaData)>
         GetAllProducerEventsAsync(Guid producerId, EventLinkParams linkParameters, bool trackChanges);

      Task<LinkResponse>
         GetProducerEventAsync(Guid producerId, Guid eventId, EventLinkParams linkParams, bool trackChanges);

      Task<EventDto>
         CreateProducerEventAsync(Guid producerId, EventForCreationDto producerEvent, bool trackChanges);

      Task UpdateProducerEventAsync
         (Guid producerId, Guid eventId, EventForUpdateDto eventForUpdate, bool trackChanges);

      Task RemoveProducerEventAsync(Guid producerId, Guid eventId, bool trackChanges);

      Task<(LinkResponse response, MetaData metaData)>
         GetAllEventsByAttendantAsync(Guid attendId, EventLinkParams linkParams, bool trackChanges);

      Task<List<DateOnly>> GetDistinctAttendantEventsDatesAsync(Guid attendantId, bool trackChanges);

   }
}
