using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.CategoryDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Presentation.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.EventManagement.Presentation.Controllers.EventControllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}/mediums/{mediumId}/categories")]
   [ApiController]
   public class CategoriesController : ControllerBase
   {
      private readonly IServiceManager _service;

      public CategoriesController(IServiceManager service) => _service = service;


      [HttpGet(Name = "GetCategories")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetAllCategories(Guid mediumId)
      {
         var linkParams = new CategoryLinkParams(HttpContext);

         var linkResponse = await _service
            .CategoryService
            .GetAllMediumCategoriesAsync(mediumId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
             Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);

      }

      [HttpGet("{id:guid}", Name = "GetCategory")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetCategory(Guid mediumId, Guid id)
      {
         var linkParams = new CategoryLinkParams(HttpContext);

         var linkResponse = await _service
            .CategoryService
            .GetMediumCategoryAsync(mediumId, id, linkParams, trackChanges: false);


         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      [HttpGet("{categoryId}/events", Name = "GetCategoryEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult>
        GetAllCategoryEvents(Guid mediumId, Guid categoryId, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);
         var (linkResponse, metaData) = await _service
            .EventService
            .GetAllCategoryEventsAsync(mediumId, categoryId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      [HttpPost(Name = "CreateCategory")]
      public async Task<IActionResult> CreateCategory
         (Guid mediumId, [FromBody] CategoryForCreationDto categoryForCreationDto)
      {
         if (categoryForCreationDto is null)
            return BadRequest("categoryForCreation object is null.");

         var createdCategory = await _service
            .CategoryService
            .CreateCategoryAsync(mediumId, categoryForCreationDto, trackChanges: false);

         return CreatedAtRoute(
            "GetCategory",
            new { mediumId, Id = createdCategory.CategoryId },
            createdCategory);
      }



      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> RemoveCategory(Guid mediumId, Guid id)
      {
         await _service
            .CategoryService
            .RemoveCategoryAsync(mediumId, id, trackChanges: false);

         return NoContent();
      }

      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateCategory
         (Guid mediumId, Guid id, [FromBody] CategoryForUpdateDto categoryForUpdateDto)
      {
         if (categoryForUpdateDto is null)
            return BadRequest("categoryForUpdate object is null.");

         await _service
            .CategoryService
            .UpdateCategoryAsync(mediumId, id, categoryForUpdateDto, trackChanges: true);

         return NoContent();
      }


      [HttpGet("{categoryId}/events/{id:guid}", Name = "GetCategoryEvent")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      public async Task<IActionResult> GetCategoryEvent
         (Guid mediumId, Guid categoryId, Guid id, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);
         var linkResponse = await _service
            .EventService
            .GetCategoryEventAsync(mediumId, categoryId, id, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);

      }
   }
}
