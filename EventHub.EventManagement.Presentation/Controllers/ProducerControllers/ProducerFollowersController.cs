using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.ProducerControllers
{
   [ApiVersion("1.0")]
   [Route("api/producers/{producerId}/followers")]
   [ApiController]
   public class ProducerFollowersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public ProducerFollowersController(IServiceManager service)
      {
         _service = service;
      }


      [HttpGet(Name = "GetProducerFollowers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllProducerFollowers
         (Guid producerId, [FromQuery] FollowerParams followerParams)
      {
         var linkParams = new FollowerLinkParams(followerParams, HttpContext);

         var (linkResponse, metaData) = await _service.
            ProducerFollowersService.
            GetAllFollowersAsync(producerId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }


      [HttpGet("{id:guid}", Name = "GetProducerFollower")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetProducerFollower
         (Guid producerId, Guid id, [FromQuery] FollowerParams followerParams)
      {
         var linkParams = new FollowerLinkParams(followerParams, HttpContext);

         var linkResponse = await _service
            .ProducerFollowersService
            .GetFollowerAsync(producerId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
             Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }



      [HttpPost(Name = "CreateProducerFollower")]
      public async Task<IActionResult> CreateProducerFollower
         (Guid producerId, [FromBody] FollowerForCreationDto followerForCreationDto)
      {
         if (followerForCreationDto is null)
            return BadRequest("followerForCreation object is null");

         var followerDto = await _service
            .ProducerFollowersService
            .CreateFollowerAsync(producerId, followerForCreationDto, trackChanges: false);

         return CreatedAtRoute(
            "GetProducerFollower",
            new { producerId, Id = followerDto.Id }
            , followerDto);
      }



      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveProducerFollower(Guid producerId, Guid id)
      {
         await _service
            .ProducerFollowersService
            .RemoveFollowerAsync(producerId, id, trackChanges: false);

         return NoContent();
      }
   }
}
