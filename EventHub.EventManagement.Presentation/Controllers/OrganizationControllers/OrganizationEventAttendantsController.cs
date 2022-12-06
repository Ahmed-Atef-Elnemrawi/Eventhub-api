using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Presentation.ActionFilter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations/{organizationId}/events/{eventId}/attendants")]
   [ApiController]
   public class OrganizationEventAttendantsController : ControllerBase
   {
      private readonly IServiceManager _serviceManager;

      public OrganizationEventAttendantsController(IServiceManager serviceManager) =>
         _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));

      /// <summary>
      /// Gets the list of all organization event attendants
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="attendantParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetOrganizationEventAttendants")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllOrganizationEventAttendants
         (Guid organizationId, Guid eventId, [FromQuery] AttendantParams attendantParams)
      {
         var linkParams = new AttendantLinkParams(attendantParams, HttpContext);

         var (linkRsponse, metaData) = await _serviceManager
            .OrganizationEventAttendantsService
            .GetAllAttendantsAsync(organizationId, eventId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkRsponse.HasLinks ?
            Ok(linkRsponse.LinkedEntities) : Ok(linkRsponse.ShapedEntities);
      }

      /// <summary>
      /// Gets the event attendant
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="id"></param>
      /// <param name="attendantParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetOrganizationEventAttendant")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetOrganizationEventAttendant
         (Guid organizationId, Guid eventId, Guid id, [FromQuery] AttendantParams attendantParams)
      {
         var linkParams = new AttendantLinkParams(attendantParams, HttpContext);

         var linkResponse = await _serviceManager
            .OrganizationEventAttendantsService
            .GetAttendantAsync(organizationId, eventId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      /// <summary>
      /// Creates a new event attendant
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="attendantForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateOrganizationEventAttendant")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> CreateOrganizationEventAttendant
         (Guid organizationId, Guid eventId, [FromBody] AttendantForCreationDto attendantForCreationDto)
      {
         var result = await new AttendantValidator().ValidateAsync(attendantForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var attendant = await _serviceManager
            .OrganizationEventAttendantsService
            .CreateAttendantAsync(organizationId, eventId, attendantForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetOrganizationEventAttendant",
            new { organizationId, eventId, Id = attendant.AttendantId },
            attendant);
      }

      /// <summary>
      /// Removes the event attendant
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}", Name = "DeleteOrganizationEventAttendant")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> RemoveOrganizationEventAttendant
         (Guid organizationId, Guid eventId, Guid id)
      {
         await _serviceManager
            .OrganizationEventAttendantsService
            .RemoveAttendantAsync(organizationId, eventId, id, trackChanges: false);

         return NoContent();
      }
   }
}
