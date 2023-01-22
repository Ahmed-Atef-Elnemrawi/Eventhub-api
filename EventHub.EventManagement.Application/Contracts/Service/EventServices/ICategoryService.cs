using EventHub.EventManagement.Application.DTOs.CategoryDtos;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.Application.Contracts.Service.EventServices
{
   public interface ICategoryService
   {
      Task<LinkResponse> GetAllMediumCategoriesAsync
         (Guid mediumId, CategoryLinkParams linkParams, bool trackChanges);

      Task<LinkResponse> GetMediumCategoryAsync
         (Guid mediumId, Guid categoryId, CategoryLinkParams linkParams, bool trackChanges);

      Task<CategoryDto> CreateCategoryAsync
         (Guid mediumId, CategoryForCreationDto category, bool trackChanges);

      Task RemoveCategoryAsync
         (Guid mediumId, Guid categoryId, bool trackChanges);

      Task UpdateCategoryAsync
         (Guid mediumId, Guid categoryId, CategoryForUpdateDto categoryForUpdate, bool trackChanges);

   }
}
