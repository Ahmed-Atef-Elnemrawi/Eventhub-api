using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/mediums")]
   [ApiController]
   public class MediumsControllers : ControllerBase
   {
      private readonly IServiceManager _service;

      public MediumsControllers(IServiceManager service)
      {
         _service = service;
      }

      [HttpGet(Name = "GetMediums")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllMediums()
      {
         var linkParams = new MediumLinkParams(HttpContext);

         var linkResponse = await _service
            .MediumService
            .GetAllMediumsAsync(linkParams, trackchanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      [HttpGet("{id}", Name = "GetMedium")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetMedium(Guid id)
      {
         var linkParams = new MediumLinkParams(HttpContext);

         var linkResponse = await _service
            .MediumService
            .GetMediumAsync(id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
             Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

   }
}
