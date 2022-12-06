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

      /// <summary>
      /// Gets the list of all producer event attendants
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventId"></param>
      /// <param name="attendantParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetProducerEventAttendants")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [Authorize(Roles = "Producer")]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      [ProducesResponseType(404)]
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

      /// <summary>
      /// Gets the event attendants
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventId"></param>
      /// <param name="id"></param>
      /// <param name="attendantParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetProducerEventAttendant")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [Authorize(Roles = "Producer")]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      [ProducesResponseType(404)]
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

      /// <summary>
      /// Creates a new event attendant
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventId"></param>
      /// <param name="attendantForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateProducerEventAttendant")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> CreateProducerEventAttendant
         (Guid producerId, Guid eventId, [FromBody] AttendantForCreationDto attendantForCreationDto)
      {
         var result = await new AttendantValidator().ValidateAsync(attendantForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var attendant = await _serviceManager
               .ProducerEventAttendantsService
               .CreateAttendantAsync(producerId, eventId, attendantForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetProducerEventAttendant",
            new { producerId, eventId, Id = attendant.AttendantId },
            attendant);
      }

      /// <summary>
      /// Removes the event attendant
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventId"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}", Name = "DeleteProducerEventAttendant")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
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
