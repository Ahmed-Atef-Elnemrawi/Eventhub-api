using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/mediums")]
   [ApiController]
   public class MediumsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public MediumsController(IServiceManager service)
      {
         _service = service;
      }

      /// <summary>
      /// Gets the list of mediums
      /// </summary>
      /// <returns></returns>
      [HttpGet(Name = "GetMediums")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllMediums()
      {
         var linkParams = new MediumLinkParams(HttpContext);

         var linkResponse = await _service
            .MediumService
            .GetAllMediumsAsync(linkParams, trackchanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      /// <summary>
      /// Gets the medium
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}", Name = "GetMedium")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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
