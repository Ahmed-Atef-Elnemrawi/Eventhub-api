using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.OrganizationControllers
{
   [ApiVersion("1.0")]
   [Route("api/organizations")]
   [ApiController]
   public class OrganizationsController : ControllerBase
   {
      private readonly IServiceManager _service;

      public OrganizationsController(IServiceManager service)
      {
         _service = service ?? throw new ArgumentNullException(nameof(service));
      }



      [HttpGet(Name = "GetOrganizations")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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



      [HttpGet("{id:guid}", Name = "GetOrganization")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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



      [HttpPost(Name = "CreateOrganization")]
      public async Task<IActionResult> CreateOrgnization
         ([FromBody] OrganizationForCreationDto organizationForCreationDto)
      {
         if (organizationForCreationDto is null)
            return BadRequest("organizationForCreation object is null.");

         var organizationDto = await _service
            .OrganizationService
            .CreateOrganizationAsync(organizationForCreationDto);

         return CreatedAtRoute("GetOrganization",
            new { Id = organizationDto.OrganizationId },
            organizationDto);
      }



      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveOrganization(Guid id)
      {
         await _service
             .OrganizationService
             .RemoveOrganizationAsync(id, trackChanges: false);

         return NoContent();
      }



      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateOrganization
         (Guid id, [FromBody] OrganizationForUpdateDto organizationForUpdateDto)
      {
         if (organizationForUpdateDto is null)
            return BadRequest("organizationForUpdate object is null.");

         await _service
            .OrganizationService
            .UpdateOrganizationAsync(id, organizationForUpdateDto, trackChanges: true);

         return NoContent();
      }

   }
}
