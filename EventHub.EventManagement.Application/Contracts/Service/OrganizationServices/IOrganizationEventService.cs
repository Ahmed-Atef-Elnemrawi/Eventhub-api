using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationEventService
   {
      Task<(LinkResponse link, MetaData metaData)>
         GetAllOrganizationEventsAsync(Guid OrganizationId, EventLinkParams linkParams, bool trackChanges);

      Task<LinkResponse>
         GetOrganizationEventAsync(Guid OrganizationId, Guid eventId, EventLinkParams linkParams, bool trackChanges);

      Task<EventDto>
         CreateOrganizationEventAsync
         (Guid OrganizationId, OrganizationEventForCreationDto OrganizationEvent, bool trackChanges);

      Task UpdateOrganizationEventAsync
         (Guid organizationId, Guid eventId, EventForUpdateDto eventForUpdate, bool trackChanges);

      Task RemoveOrganizationEventAsync
         (Guid organizationId, Guid eventId, bool trackChanges);
   }
}
