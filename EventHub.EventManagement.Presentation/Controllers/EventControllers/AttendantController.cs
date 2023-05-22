using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/attendants")]
   [ApiController]
   public class AttendantController : ControllerBase
   {
      private readonly IServiceManager _service;

      public AttendantController(IServiceManager service)
      {
         _service = service;
      }

      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [HttpGet("{attendantId:guid}/events-I-attend", Name = "GetAttendantEvents")]
      public async Task<IActionResult> GetAttendantEvents(Guid attendantId, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);

         var (linkResponse, metaData) = await _service.ProducerEventService
            .GetAllEventsByAttendantAsync(attendantId, linkParams, trackChanges: false);

         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }


      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [HttpGet("{attendantId:guid}/distinct-events-I-attend-dates", Name = "getDistinctEventDates")]
      public async Task<IActionResult> GetDistinctAttendantEventsDates([FromRoute] Guid attendantId)
      {
         var dateTimes = await _service.ProducerEventService
            .GetDistinctAttendantEventsDatesAsync(attendantId, trackChanges: false);

         if (dateTimes is null)
            return NotFound();

         return Ok(dateTimes);
      }
   }
}
