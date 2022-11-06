using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence
{
   public interface ICategoryRepository
   {
      Task<IEnumerable<Category>> GetMediumCategoriesAsync(Guid mediumId, bool trackChanges);
      Task<Category?> GetMediumCategoryAsync(Guid meidumId, Guid categoryId, bool trackChanges);
      Task<Category?> GetCategoryAsync(Guid CategoryId, bool trackChanges);
      void CreateCategory(Guid MediumId, Category category);
      void RemoveCategory(Category category);
   }
}
