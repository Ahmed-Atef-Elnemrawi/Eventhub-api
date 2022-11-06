using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations/{organizationId}/events")]
   [ApiController]
   public class OrganizationEventController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationEventController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));
      }

      [HttpGet(Name = "GetOrganizationEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult>
         GetAllOrganizationEvents(Guid organizationId, [FromQuery] EventParams eventParameters)
      {
         var linkParams = new EventLinkParams(eventParameters, HttpContext);

         var (linkResponse, metaData) =
            await _service.OrganizationEventService
            .GetAllOrganizationEventsAsync(organizationId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }


      [HttpGet("{id:guid}", Name = "GetOrganizationEvent")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetOrganizationEvent
         (Guid organizationId, Guid id, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);

         var linkResponse = await
            _service.OrganizationEventService
            .GetOrganizationEventAsync(organizationId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      [HttpPost(Name = "CreateOrganizationEvent")]
      public async Task<IActionResult> CreateOrganizationEvent
         (Guid organizationId, [FromBody] OrganizationEventForCreationDto eventForCreationDto)
      {
         if (eventForCreationDto is null)
            return BadRequest("eventForCreation object is null.");

         var eventDto = await
            _service.OrganizationEventService
            .CreateOrganizationEventAsync(organizationId, eventForCreationDto, trackChanges: false);

         return CreatedAtRoute("GetOrganizationEvent",
            new { organizationId, Id = eventDto.EventId }
            , eventDto);
      }

      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveOrganizationEvent(Guid organizationId, Guid id)
      {
         await _service
            .OrganizationEventService
            .RemoveOrganizationEventAsync(organizationId, id, trackChanges: false);

         return NoContent();
      }


      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateOrganizationEvent
         (Guid organizationId, Guid id, [FromBody] EventForUpdateDto eventForUpdateDto)
      {
         if (eventForUpdateDto is null)
            return BadRequest("eventForUpdate object is null.");

         await _service
            .OrganizationEventService
            .UpdateOrganizationEventAsync(organizationId, id, eventForUpdateDto, trackChanges: true);

         return NoContent();
      }




   }
}
