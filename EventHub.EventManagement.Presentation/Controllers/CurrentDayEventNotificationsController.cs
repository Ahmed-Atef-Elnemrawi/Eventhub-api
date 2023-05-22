using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/{userId:guid}/current-day-events")]
   [ApiController]
   public class CurrentDayEventNotificationsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public CurrentDayEventNotificationsController(IServiceManager service)
      {
         _service = service;
      }

      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [HttpGet()]
      public async Task<IActionResult> GetCurrentDayEvents
         ([FromRoute] Guid userId, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);
         var response = await _service
            .ProducerEventAttendantsService
            .GetAttendantCurrentDayEvents(userId, linkParams, trackChanges: false);

         return response.HasLinks
            ? Ok(response.LinkedEntities)
            : Ok(response.ShapedEntities);
      }

      [HttpGet("count")]
      public async Task<IActionResult> GetCurrenDayEventsCount
         ([FromRoute] Guid userId)
      {
         var count = await _service.ProducerEventAttendantsService
            .GetAttendantCurrentDayEventsCount(userId, trackChanges: false);

         return Ok(new { count });
      }
   }
}
