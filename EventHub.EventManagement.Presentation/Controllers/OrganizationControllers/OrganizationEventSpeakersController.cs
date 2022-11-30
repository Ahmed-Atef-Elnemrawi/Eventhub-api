using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations/{organizationId}/events/{eventId}/speakers")]
   [ApiController]
   public class OrganizationEventSpeakersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationEventSpeakersController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));
      }

      [HttpGet(Name = "GetOrganizationEventSpeakers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllOrganizationEventSpeakers
         (Guid organizationId, Guid eventId, [FromQuery] SpeakerParams speakerParam)
      {
         var linkParams = new SpeakerLinkParams(speakerParam, HttpContext);

         var linkResponse = await _service
             .OrganizationEventSpeakerService
             .GetAllEventSpeakersAsync(organizationId, eventId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      [HttpGet("{id:guid}", Name = "GetEventSpeaker")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetOrganizationEventSpeaker
         (Guid organizationId, Guid eventId, Guid Id, [FromQuery] SpeakerParams speakerParams)
      {
         var linkParams = new SpeakerLinkParams(speakerParams, HttpContext);

         var linkResponse = await _service
            .OrganizationEventSpeakerService
            .GetEventSpeaker(organizationId, eventId, Id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      [HttpPost(Name = "CreateOrganizationEventSpeaker")]
      [Authorize(Roles = "Organization")]
      public async Task<IActionResult> AddOrganizationEventSpeaker
         (Guid organizationId, Guid eventId, EventSpeakerForCreationDto eventSpeakerForCreationDto)
      {

         await _service.OrganizationEventSpeakerService
            .AddEventSpeakerAsync(organizationId, eventId, eventSpeakerForCreationDto, trackChanges: false);

         return Ok();
      }

      [HttpDelete("{id:guid}")]
      [Authorize(Roles = "Organization")]
      public async Task<IActionResult> RemoveOrganizationEventSpeaker
         (Guid organizationId, Guid eventId, Guid Id)
      {
         await _service.OrganizationEventSpeakerService
            .RemoveEventSpeakerAsync(organizationId, eventId, Id, trackChanges: false);

         return NoContent();
      }
   }
}
