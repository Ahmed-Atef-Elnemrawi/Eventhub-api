using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.OrganizationServices
{
   public interface IOrganizationService
   {
      Task<(LinkResponse link, MetaData metaData)>
         GetAllOrganizationsAsync(OrganizationLinkParams organizationLinkParams, bool trackChanges);

      Task<LinkResponse> GetOrganizationAsync(Guid id, OrganizationLinkParams linkParams, bool trackChanges);

      Task<OrganizationDto> CreateOrganizationAsync(OrganizationForCreationDto organization);

      Task UpdateOrganizationAsync
         (Guid organizationId, OrganizationForUpdateDto organizationForUpdate, bool trackChanges);

      Task RemoveOrganizationAsync(Guid organizationId, bool trackChanges);

   }
}
