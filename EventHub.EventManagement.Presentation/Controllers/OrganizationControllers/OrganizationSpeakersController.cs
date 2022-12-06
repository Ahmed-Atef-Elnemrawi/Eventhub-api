using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Presentation.ActionFilter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations/{organizationId}/speakers")]
   [ApiController]
   public class OrganizationSpeakersController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationSpeakersController(IServiceManager service) =>
         _service = service ?? throw new ArgumentNullException(nameof(service));

      /// <summary>
      /// Gets the list of organization speakers
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="speakerParam"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetOrganizationSpeakers")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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


      /// <summary>
      /// Gets the speaker
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="speakerId"></param>
      /// <param name="speakerParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetSpeaker")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
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

      /// <summary>
      /// Creates a new speaker
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="speakerForCreation"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateOrganizationSpeaker")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> CreateOrganizationSpeaker
         (Guid organizationId, [FromBody] SpeakerForCreationDto speakerForCreation)
      {
         var result = await new SpeakerValidator().ValidateAsync(speakerForCreation);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var speaker = await _service
            .OrganizationSpeakerService
            .CreateOrganizationSpeakerAsync(organizationId, speakerForCreation, trackChanges: false);

         return CreatedAtRoute("GetSpeaker", new { organizationId, Id = speaker.SpeakerId }, speaker);
      }

      /// <summary>
      /// Updates the speaker
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="Id"></param>
      /// <param name="speakerForUpdate"></param>
      /// <returns></returns>
      [HttpPut("{id:guid}")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> UpdateOrganizationSpeaker
         (Guid organizationId, Guid Id, [FromBody] SpeakerForUpdateDto speakerForUpdate)
      {
         var result = await new SpeakerValidator().ValidateAsync(speakerForUpdate);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         await _service.OrganizationSpeakerService
               .UpdateOrganizationSpeakerAsync(organizationId, Id, speakerForUpdate, trackChanges: true);

         return NoContent();
      }

      /// <summary>
      /// Removes the speaker
      /// </summary>
      /// <param name="organizationId"></param>
      /// <param name="Id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> RemoveOrganizationSpeaker(Guid organizationId, Guid Id)
      {
         await _service.OrganizationSpeakerService
            .RemoveOrganizationSpeakerAsync(organizationId, Id, trackChanges: false);

         return NoContent();
      }
   }
}
