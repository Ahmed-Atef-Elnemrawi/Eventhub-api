using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.SpeakerDtos;
using EventHub.EventManagement.Application.Models;
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

      /// <summary>
      /// Gets the list of event speakrs
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="speakerParam"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetOrganizationEventSpeakers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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

      /// <summary>
      /// Gets the event speaker
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="Id"></param>
      /// <param name="speakerParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetEventSpeaker")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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
      /// <summary>
      /// Links an speaker to the event
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="eventSpeakerForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateOrganizationEventSpeaker")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> AddOrganizationEventSpeaker
         (Guid organizationId, Guid eventId, EventSpeakerForCreationDto eventSpeakerForCreationDto)
      {

         await _service.OrganizationEventSpeakerService
            .AddEventSpeakerAsync(organizationId, eventId, eventSpeakerForCreationDto, trackChanges: false);

         return Ok();
      }
      /// <summary>
      /// Unlinks an speaker from the event
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="eventId"></param>
      /// <param name="Id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> RemoveOrganizationEventSpeaker
         (Guid organizationId, Guid eventId, Guid Id)
      {
         await _service.OrganizationEventSpeakerService
            .RemoveEventSpeakerAsync(organizationId, eventId, Id, trackChanges: false);

         return NoContent();
      }
   }
}
