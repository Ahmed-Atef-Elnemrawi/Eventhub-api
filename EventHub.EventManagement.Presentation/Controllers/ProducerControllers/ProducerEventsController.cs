using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
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

        [HttpGet(Name = "GetProducerEvents")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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


        [HttpGet("{id:guid}", Name = "GetProducerEvent")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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


        [HttpPost(Name = "CreateProducerEvent")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> CreateProducerEvent
           (Guid producerId, [FromBody] EventForCreationDto eventForCreationDto)
        {
            if (eventForCreationDto is null)
                return BadRequest("eventForCreation object is null");

            var eventDto = await _service
               .ProducerEventService
               .CreateProducerEventAsync(producerId, eventForCreationDto, trackChanges: false);

            return CreatedAtRoute(
               "GetProducerEvent",
               new { producerId, Id = eventDto.EventId },
               eventDto);
        }


        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> RemoveProducerEvent(Guid producerId, Guid id)
        {
            await _service
                .ProducerEventService
                .RemoveProducerEventAsync(producerId, id, trackChanges: false);

            return NoContent();
        }


        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Producer")]
        public async Task<IActionResult> UpdateProducerEvent
           (Guid producerId, Guid id, [FromBody] EventForUpdateDto eventForUpdateDto)
        {
            if (eventForUpdateDto is null)
                return BadRequest("eventForUpdate object is null.");

            await _service
               .ProducerEventService
               .UpdateProducerEventAsync(producerId, id, eventForUpdateDto, trackChanges: true);

            return NoContent();
        }
    }
}
