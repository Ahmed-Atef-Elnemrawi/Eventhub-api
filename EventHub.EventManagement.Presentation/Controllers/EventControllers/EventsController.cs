using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/events")]
   [ApiController]
   public class EventsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public EventsController(IServiceManager service)
      {
         _service = service;
      }

      [HttpGet(Name = "GetEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetEvents([FromQuery] EventParams eventParams)
      {
         var (events, metaData) = await _service
            .EventService
            .GetAllEventsAsync(eventParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return Ok(events);
      }

      [HttpGet("{id:guid}")]
      public async Task<IActionResult> GetEvent(Guid id)
      {
         var eventDto = await _service
            .EventService
            .GetEventAsync(id, trackChanges: false);

         return Ok(eventDto);
      }
   }
}
