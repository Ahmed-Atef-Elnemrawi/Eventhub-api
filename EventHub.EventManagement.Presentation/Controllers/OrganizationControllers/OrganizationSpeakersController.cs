using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/organizations/{organizationId}/speakers")]
   [ApiController]
   public class OrganizationSpeakersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationSpeakersController(IServiceManager service) =>
         _service = service ?? throw new ArgumentNullException(nameof(service));


      [HttpGet(Name = "GetOrganizationSpeakers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllOrganizationSpeakers
         (Guid organizationId, [FromQuery] SpeakerParams speakerParam)
      {
         var linkParams = new SpeakerLinkParams(speakerParam, HttpContext);

         var linkResponse = await _service
            .OrganizationSpeakerService
            .GetAllOrganizationSpeakers(organizationId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }


      [HttpGet("{id:guid}", Name = "GetSpeaker")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetOrganizationSpeaker
         (Guid organizationId, Guid speakerId, [FromQuery] SpeakerParams speakerParams)
      {
         var linkParams = new SpeakerLinkParams(speakerParams, HttpContext);

         var linkResponse = await _service
            .OrganizationSpeakerService
            .GetOrganizationSpeaker(organizationId, speakerId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
           Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }


      [HttpPost(Name = "CreateOrganizationSpeaker")]
      public async Task<IActionResult> CreateOrganizationSpeaker
         (Guid organizationId, [FromBody] SpeakerForCreationDto speakerForCreation)
      {
         if (speakerForCreation is null)
            return BadRequest("SpeakerForCreation object is null.");

         var speaker = await _service
            .OrganizationSpeakerService
            .CreateOrganizationSpeakerAsync(organizationId, speakerForCreation, trackChanges: false);

         return CreatedAtRoute("GetSpeaker", new { organizationId, Id = speaker.SpeakerId }, speaker);
      }

      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateOrganizationSpeaker
         (Guid organizationId, Guid Id, [FromBody] SpeakerForUpdateDto speakerForUpdate)
      {
         if (speakerForUpdate is null)
            return BadRequest("SpeakerForUpdate object is null.");

         await _service.OrganizationSpeakerService
            .UpdateOrganizationSpeakerAsync(organizationId, Id, speakerForUpdate, trackChanges: true);

         return NoContent();
      }

      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveOrganizationSpeaker(Guid organizationId, Guid Id)
      {
         await _service.OrganizationSpeakerService
            .RemoveOrganizationSpeakerAsync(organizationId, Id, trackChanges: false);

         return NoContent();
      }
   }
}
