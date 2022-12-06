using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Presentation.ActionFilter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/organizations")]
   [ApiController]
   public class OrganizationsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationsController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));

      }


      /// <summary>
      /// Gets the list of events organizations
      /// </summary>
      /// <param name="organizationParams"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetOrganizations")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllOrganizations([FromQuery] OrganizationParams organizationParams)
      {
         var linkParams = new OrganizationLinkParams(organizationParams, HttpContext: HttpContext);
         var (linkResponse, metaData) = await _service
            .OrganizationService
            .GetAllOrganizationsAsync(linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);

      }


      /// <summary>
      /// Gets the organization
      /// </summary>
      /// <param name="id"></param>
      /// <param name="organizationParams"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetOrganization")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetOrganization
         (Guid id, [FromQuery] OrganizationParams organizationParams)
      {
         var linkParams = new OrganizationLinkParams(organizationParams, HttpContext);

         var linkResponse = await _service
            .OrganizationService
            .GetOrganizationAsync(id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      /// <summary>
      /// Creates a new organization
      /// </summary>
      /// <param name="organizationForCreationDto"></param>
      /// <returns></returns>
      [HttpPost(Name = "CreateOrganization")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> CreateOrgnization
         ([FromBody] OrganizationForCreationDto organizationForCreationDto)
      {
         var result = await new OrganizationValidator().ValidateAsync(organizationForCreationDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         var organizationDto = await _service
            .OrganizationService
            .CreateOrganizationAsync(organizationForCreationDto);

         return CreatedAtRoute("GetOrganization",
            new { Id = organizationDto.OrganizationId },
            organizationDto);
      }


      /// <summary>
      /// Removes the organization
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id:guid}", Name = "RemoveOrganization")]
      [Authorize(Roles = "Administrator,Organization")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> RemoveOrganization(Guid id)
      {
         await _service
             .OrganizationService
             .RemoveOrganizationAsync(id, trackChanges: false);

         return NoContent();
      }


      /// <summary>
      /// Updates the organization
      /// </summary>
      /// <param name="id"></param>
      /// <param name="organizationForUpdateDto"></param>
      /// <returns></returns>
      [HttpPut("{id:guid}")]
      [Authorize(Roles = "Organization")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(401)]
      [ProducesResponseType(403)]
      public async Task<IActionResult> UpdateOrganization
         (Guid id, [FromBody] OrganizationForUpdateDto organizationForUpdateDto)
      {
         var result = await new OrganizationValidator().ValidateAsync(organizationForUpdateDto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

         await _service
            .OrganizationService
            .UpdateOrganizationAsync(id, organizationForUpdateDto, trackChanges: true);

         return NoContent();
      }

   }
}
