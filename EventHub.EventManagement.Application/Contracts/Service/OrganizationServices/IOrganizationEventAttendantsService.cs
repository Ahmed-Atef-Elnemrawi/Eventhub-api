using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationEventAttendantsService
   {
      Task<(LinkResponse link, MetaData metaData)> GetAllAttendantsAsync
         (Guid organizationId, Guid eventId, AttendantLinkParams attendantLinkParams, bool trackChanges);

      Task<LinkResponse> GetAttendantAsync
         (Guid organizationId, Guid eventId, Guid attendantId, AttendantLinkParams linkParams, bool trackChanges);

      Task<AttendantDto> CreateAttendantAsync
         (Guid organizationId, Guid eventId, AttendantForCreationDto attendantForCreationDto, bool trackChanges);

      Task RemoveAttendantAsync(Guid organizationId, Guid eventId, Guid attendantId, bool trackChanges);
   }
}
