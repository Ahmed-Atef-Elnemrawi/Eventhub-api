using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.ProducerControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/producers/{producerId}/events/{eventId}/Attendants")]
   [ApiController]
   public class ProducerEventAttendantsController : ControllerBase
   {
      private readonly IServiceManager _serviceManager;

      public ProducerEventAttendantsController(IServiceManager serviceManager) =>
         _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));

      [HttpGet(Name = "GetProducerEventAttendants")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetProducerEventAllAttendants
         (Guid producerId, Guid eventId, [FromQuery] AttendantParams attendantParams)
      {
         var linkParams = new AttendantLinkParams(attendantParams, HttpContext);

         var (linkResponse, metaData) = await _serviceManager
            .ProducerEventAttendantsService
            .GetAllAttendantsAsync(producerId, eventId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }


      [HttpGet("{id:guid}", Name = "GetProducerEventAttendant")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetProducerEventAttendant
         (Guid producerId, Guid eventId, Guid id, [FromQuery] AttendantParams attendantParams)
      {
         var linkParams = new AttendantLinkParams(attendantParams, HttpContext);

         var linkResponse = await _serviceManager
            .ProducerEventAttendantsService
            .GetAttendantAsync(producerId, eventId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }


      [HttpPost(Name = "CreateProducerEventAttendant")]
      public async Task<IActionResult> CreateProducerEventAttendant
         (Guid producerId, Guid eventId, [FromBody] AttendantForCreationDto attendantForCreationDto)
      {
         if (attendantForCreationDto is null)
            return BadRequest("attendantForCreation object is null.");

         var attendant = await _serviceManager
            .ProducerEventAttendantsService
            .CreateAttendantAsync(producerId, eventId, attendantForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetProducerEventAttendant",
            new { producerId, eventId, Id = attendant.AttendantId },
            attendant);
      }


      [HttpDelete("{id:guid}", Name = "DeleteProducerEventAttendant")]
      public async Task<IActionResult> RemoveProducerEventAttendant
         (Guid producerId, Guid eventId, Guid id)
      {
         await _serviceManager
            .ProducerEventAttendantsService
            .RemoveAttendantAsync(producerId, eventId, id, trackChanges: false);

         return NoContent();
      }
   }
}
