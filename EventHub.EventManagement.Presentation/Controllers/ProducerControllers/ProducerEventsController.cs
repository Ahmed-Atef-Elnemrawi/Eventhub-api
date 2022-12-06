using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.EventDto;
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
   [Route("api/v{v:apiversion}/producers/{producerId}/events")]
   [ApiController]
   public class ProducerEventsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public ProducerEventsController(IServiceManager service)
      {
         _service = service;
      }
      /// <summary>
      /// Gets the list of all producer events
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetProducerEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllProducerEvents
         (Guid producerId, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);
         var (linkResponse, metaData) = await _service
            .ProducerEventService
             .GetAllProducerEventsAsync(producerId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      /// <summary>
      /// Gets the producer event
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="id"></param>
      /// <param name="eventParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetProducerEvent")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetProducerEvent
         (Guid producerId, Guid id, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);

         var linkResponse = await _service
            .ProducerEventService
            .GetProducerEventAsync(producerId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      /// <summary>
      /// Creates a new producer event
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="eventForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateProducerEvent")]
      [Authorize(Roles = "Producer")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> CreateProducerEvent
         (Guid producerId, [FromBody] EventForCreationDto eventForCreationDto)
      {
         var result = await new EventValidator().ValidateAsync(eventForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var eventDto = await _service
               .ProducerEventService
               .CreateProducerEventAsync(producerId, eventForCreationDto, trackChanges: false);

         return CreatedAtRoute(
            "GetProducerEvent",
            new { producerId, Id = eventDto.EventId },
            eventDto);
      }


      /// <summary>
      /// Removes the producer event
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}")]
      [Authorize(Roles = "Producer")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> RemoveProducerEvent(Guid producerId, Guid id)
      {
         await _service
             .ProducerEventService
             .RemoveProducerEventAsync(producerId, id, trackChanges: false);

         return NoContent();
      }

      /// <summary>
      /// Updates the producer event
      /// </summary>
      /// <param name="producerId"></param>
      /// <param name="id"></param>
      /// <param name="eventForUpdateDto"></param>
      /// <returns></returns>
      [HttpPut("{id:guid}")]
      [Authorize(Roles = "Producer")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> UpdateProducerEvent
         (Guid producerId, Guid id, [FromBody] EventForUpdateDto eventForUpdateDto)
      {
         var result = await new EventValidator().ValidateAsync(eventForUpdateDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         await _service
               .ProducerEventService
               .UpdateProducerEventAsync(producerId, id, eventForUpdateDto, trackChanges: true);

         return NoContent();
      }
   }
}
