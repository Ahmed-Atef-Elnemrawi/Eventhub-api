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

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations/{organizationId}/followers")]
   [ApiController]
   public class OrganizationFollowersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationFollowersController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));
      }

      /// <summary>
      /// Gets the list of organization followers
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="followerParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetOrganizationFollowers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllOrganizationFollowers
         (Guid organizationId, [FromQuery] FollowerParams followerParams)
      {
         var linkParams = new FollowerLinkParams(followerParams, HttpContext);

         var (linkResponse, metaData) = await _service
            .OrganizationFollowersService
            .GetAllFollowersAsync(organizationId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      /// <summary>
      /// Gets the organization follower
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="id"></param>
      /// <param name="followerParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetOrganizationFollower")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetOrganizationFollower
         (Guid organizationId, Guid id, [FromQuery] FollowerParams followerParams)
      {
         var linkParams = new FollowerLinkParams(followerParams, HttpContext);

         var linkResponse = await _service
            .OrganizationFollowersService
            .GetFollowerAsync(organizationId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }


      /// <summary>
      /// Creates a new organization follower
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="followerForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateOrganizationFollower")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> CreateOrganizationFollower
         (Guid organizationId, [FromBody] FollowerForCreationDto followerForCreationDto)
      {
         var result = await new FollowerValidator().ValidateAsync(followerForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var followerDto = await _service
            .OrganizationFollowersService
            .CreateFollowerAsync(organizationId, followerForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetOrganizationFollower",
            new { organizationId, followerDto.FollowerId },
            followerDto);
      }


      /// <summary>
      /// Removes the organization follower
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> RemoveOrganizationFollower(Guid organizationId, Guid id)
      {
         await _service
            .OrganizationFollowersService
            .RemoveFollowerAsync(organizationId, id, trackChanges: false);

         return NoContent();
      }
   }
}
