using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;

namespace EventHub.EventManagement.Application.Contracts.Service.EventServices
{
   public interface IAttendantService
   {
      Task<(IEnumerable<ShapedEntity> attendants, MetaData metaData)>
         GetAllAttendantsAsync(Guid eventId, AttendantParams attendantParams, bool trackChanges);
      Task<AttendantDto> GetAttendantAsync(Guid eventId, Guid attendantId, bool trackChanges);
      Task<AttendantDto> CreateAttendantAsync(Guid eventId, AttendantForCreationDto attendant, bool trackChanges);
      Task RemoveAttendantAsync(Guid eventId, Guid attendantId, bool trackChange);
   }
}
