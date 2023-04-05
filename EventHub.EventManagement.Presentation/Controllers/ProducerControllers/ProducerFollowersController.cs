using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Presentation.ActionFilter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.ProducerControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/producers/{producerId}/followers")]
   [ApiController]
   public class ProducerFollowersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public ProducerFollowersController(IServiceManager service)
      {
         _service = service;
      }

      /// <summary>
      /// Gets the list of producer followers
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="followerParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetProducerFollowers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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

      /// <summary>
      /// Gets the producer follower
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="followerId"></param>
      /// <param name="followerParams"></param>
      /// <returns></returns>
      [HttpGet("{followerId:guid}", Name = "GetProducerFollower")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetProducerFollower
         ([FromRoute] Guid producerId, [FromRoute] Guid followerId, [FromQuery] FollowerParams followerParams)
      {
         var linkParams = new FollowerLinkParams(followerParams, HttpContext);

         var linkResponse = await _service
            .ProducerFollowersService
            .GetFollowerAsync(producerId, followerId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
             Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }


      /// <summary>
      /// Creates a new producer follower
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="followerForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateProducerFollower")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> CreateProducerFollower
         (Guid producerId, [FromBody] FollowerForCreationDto followerForCreationDto)
      {
         var result = await new FollowerValidator().ValidateAsync(followerForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var followerDto = await _service
            .ProducerFollowersService
            .CreateFollowerAsync(producerId, followerForCreationDto, trackChanges: false);

         return CreatedAtRoute(
            "GetProducerFollower",
            new { producerId, followerDto.FollowerId }
            , followerDto);
      }


      /// <summary>
      /// Removes the producer follower
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="followerId"></param>
      /// <returns></returns>
      [HttpDelete("{followerId:guid}", Name = "RemoveProducerFollower")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> RemoveProducerFollower(Guid producerId, [FromRoute] Guid followerId)
      {
         await _service
            .ProducerFollowersService
            .RemoveFollowerAsync(producerId, followerId, trackChanges: false);

         return NoContent();
      }


      [HttpGet("followers-count", Name = "GetProducerFollowersCount")]
      public async Task<IActionResult> GetProducerFollowersCount([FromRoute] Guid producerId)
      {
         var count = await _service.ProducerFollowersService
             .GetProducerFollowersCountAsync(producerId, trackChanges: false);

         return Ok(new { followersCount = count });
      }
   }
}
