using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/events")]
   [ApiController]
   public class EventsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public EventsController(IServiceManager service)
      {
         _service = service;
      }

      /// <summary>
      /// Gets the list of all events
      /// </summary>
      /// <param name="eventParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetEvents([FromQuery] EventParams eventParams)
      {
         var (events, metaData) = await _service
            .EventService
            .GetAllEventsAsync(eventParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return Ok(events);
      }
      /// <summary>
      /// Gets the event
      /// </summary>
      /// <param name="id"></param>
      /// <param name="fields">comma separated string fields</param>
      /// <returns></returns>
      [HttpGet("{id:guid}")]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetEvent(Guid id, [FromQuery] string fields)
      {
         var eventToReturn = await _service
            .EventService
            .GetEventAsync(id, fields, trackChanges: false);

         return Ok(eventToReturn);
      }
   }
}
