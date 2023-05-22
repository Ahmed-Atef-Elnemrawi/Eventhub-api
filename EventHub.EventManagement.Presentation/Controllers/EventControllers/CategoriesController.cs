using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Models;
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

      /// <summary>
      /// Gets the list of all categoryies of the medium
      /// </summary>
      /// <param name="mediumId"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetCategories")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetAllCategories(Guid mediumId)
      {
         var linkParams = new CategoryLinkParams(HttpContext);

         var linkResponse = await _service
            .CategoryService
            .GetAllMediumCategoriesAsync(mediumId, linkParams, trackChanges: false);

         return linkResponse.HasLinks ?
             Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);

      }

      /// <summary>
      /// Gets the category of the medium
      /// </summary>
      /// <param name="mediumId"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id:guid}", Name = "GetCategory")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetCategory(Guid mediumId, Guid id)
      {
         var linkParams = new CategoryLinkParams(HttpContext);

         var linkResponse = await _service
            .CategoryService
            .GetMediumCategoryAsync(mediumId, id, linkParams, trackChanges: false);


         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntity) : Ok(linkResponse.ShapedEntity);
      }

      /// <summary>
      /// Gets the list of category events
      /// </summary>
      /// <param name="mediumId"></param>
      /// <param name="categoryId"></param>
      /// <param name="eventParams"></param>
      /// <returns></returns>
      [HttpGet("{categoryId}/events", Name = "GetCategoryEvents")]
      [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      [ProducesResponseType(200, Type = typeof(ShapedEntity))]
      [ProducesResponseType(404)]
      public async Task<IActionResult>
        GetAllCategoryEvents(Guid mediumId, Guid categoryId, [FromQuery] EventParams eventParams)
      {
         var linkParams = new EventLinkParams(eventParams, HttpContext);
         var (linkResponse, metaData) = await _service
            .EventService
            .GetAllCategoryProducerEventsAsync(mediumId, categoryId, linkParams, trackChanges: false);

         Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(metaData));

         return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) : Ok(linkResponse.ShapedEntities);
      }

      //[HttpPost(Name = "CreateCategory")]
      //[Authorize(Roles = "Administrator")]
      //public async Task<IActionResult> CreateCategory
      //   (Guid mediumId, [FromBody] CategoryForCreationDto categoryForCreationDto)
      //{
      //   if (categoryForCreationDto is null)
      //      return BadRequest("categoryForCreation object is null.");

      //   if (!ModelState.IsValid)
      //   {
      //      return BadRequest(ModelState);
      //   }

      //   var createdCategory = await _service
      //      .CategoryService
      //      .CreateCategoryAsync(mediumId, categoryForCreationDto, trackChanges: false);

      //   return CreatedAtRoute(
      //      "GetCategory",
      //      new { mediumId, Id = createdCategory.CategoryId },
      //      createdCategory);
      //}



      //[HttpDelete("{id:guid}")]
      //[Authorize(Roles = "Administrator")]
      //public async Task<IActionResult> RemoveCategory(Guid mediumId, Guid id)
      //{
      //   await _service
      //      .CategoryService
      //      .RemoveCategoryAsync(mediumId, id, trackChanges: false);

      //   return NoContent();
      //}

      //[HttpPut("{id:guid}")]
      //[Authorize(Roles = "Organization, Producer")]
      //public async Task<IActionResult> UpdateCategory
      //   (Guid mediumId, Guid id, [FromBody] CategoryForUpdateDto categoryForUpdateDto)
      //{
      //   if (categoryForUpdateDto is null)
      //      return BadRequest("categoryForUpdate object is null.");

      //   await _service
      //      .CategoryService
      //      .UpdateCategoryAsync(mediumId, id, categoryForUpdateDto, trackChanges: true);

      //   return NoContent();
      //}


   }
}
