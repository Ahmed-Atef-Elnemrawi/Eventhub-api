using EventHub.EventManagement.Application.DTOs.MediumDto;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.Application.Contracts.Service.EventServices
{
   public interface IMediumService
   {
      Task<LinkResponse> GetAllMediumsAsync(MediumLinkParams linkParams, bool trackchanges);
      Task<LinkResponse> GetMediumAsync(Guid mediumId, MediumLinkParams linkParams, bool trackChanges);
   }
}
