using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.ProducerServices
{
   public interface IProducerEventAttendantsService
   {
      Task<(LinkResponse link, MetaData metaData)> GetAllAttendantsAsync
         (Guid producerId, Guid eventId, AttendantLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetAttendantAsync
         (Guid producerId, Guid eventId, Guid attendantId, AttendantLinkParams linkParams, bool trackChanges);

      Task<AttendantDto> CreateAttendantAsync
         (Guid producerId, Guid eventId, AttendantForCreationDto attendantForCreationDto, bool trackChanges);

      Task RemoveAttendantAsync(Guid producerId, Guid eventId, Guid attendantId, bool trackChanges);

      Task<LinkResponse> GetAttendantCurrentDayEvents(Guid attendantId, EventLinkParams linkParams, bool trackChanges);

      Task<int> GetAttendantCurrentDayEventsCount(Guid attendantId, bool trackChanges);
   }
}
