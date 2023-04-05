using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/followers/{followerId}/producers")]
   [ApiController]
   public class FollowersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public FollowersController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));
      }

      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(LinkResponse))]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [HttpGet(Name = "getProducersByFollowerId")]
      public async Task<IActionResult> GetProducersByFollwerId
         ([FromRoute] Guid followerId, [FromQuery] ProducerParams producerParams)
      {

         var linkParams = new ProducerLinkParams(producerParams, HttpContext);
         var linkResponse = await _service.ProducerService.GetAllProducersAsync
            (followerId, linkParams, trackChanges: false);

         if (linkResponse is null)
            return NotFound();

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      [ProducesResponseType(200)]
      [HttpGet("{producerId:guid}")]
      public async Task<IActionResult> ChechIfUserFollowsProducer
         (Guid followerId, [FromRoute] Guid producerId)
      {

         var result = await _service.ProducerService.CheckIfProducerExist
            (followerId, producerId, trackChanges: false);

         var response = new { result };


         return Ok(response);

      }

      [ProducesResponseType(204)]
      [HttpDelete("{producerId:guid}")]
      public async Task<IActionResult> DeleteProducerByFollowerId(Guid producerId, Guid followerId)
      {
         await _service.ProducerFollowersService.RemoveProducerFollowerAsync(producerId, followerId);
         return NoContent();
      }
      //ToDo: create an endpoint for organization also
   }
}
