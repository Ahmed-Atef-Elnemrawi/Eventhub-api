using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
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


      [HttpGet(Name = "GetOrganizationFollowers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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

      [HttpGet("{id:guid}", Name = "GetOrganizationFollower")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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



      [HttpPost(Name = "CreateOrganizationFollower")]
      public async Task<IActionResult> CreateOrganizationFollower
         (Guid organizationId, [FromBody] FollowerForCreationDto followerForCreationDto)
      {
         if (followerForCreationDto is null)
            return BadRequest("followerForCreation object is null.");

         var followerDto = await _service
            .OrganizationFollowersService
            .CreateFollowerAsync(organizationId, followerForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetOrganizationFollower",
            new { organizationId, followerDto.Id },
            followerDto);
      }



      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveOrganizationFollower(Guid organizationId, Guid id)
      {
         await _service
            .OrganizationFollowersService
            .RemoveFollowerAsync(organizationId, id, trackChanges: false);

         return NoContent();
      }
   }
}
