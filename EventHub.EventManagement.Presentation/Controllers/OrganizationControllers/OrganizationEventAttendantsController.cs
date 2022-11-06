using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
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

      [HttpGet(Name = "GetOrganizationEventAttendants")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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


      [HttpGet("{id:guid}", Name = "GetOrganizationEventAttendant")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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


      [HttpPost(Name = "CreateOrganizationEventAttendant")]
      public async Task<IActionResult> CreateOrganizationEventAttendant
         (Guid organizationId, Guid eventId, [FromBody] AttendantForCreationDto attendantForCreationDto)
      {
         if (attendantForCreationDto is null)
            return BadRequest("attendantForCreation object is null.");

         var attendant = await _serviceManager
            .OrganizationEventAttendantsService
            .CreateAttendantAsync(organizationId, eventId, attendantForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetOrganizationEventAttendant",
            new { organizationId, eventId, Id = attendant.AttendantId },
            attendant);
      }


      [HttpDelete("{id:guid}", Name = "DeleteOrganizationEventAttendant")]
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
