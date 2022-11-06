using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.EventRepositories
{
   public sealed class CategoriesRepository : BaseRepository<Category>, ICategoryRepository
   {
      public CategoriesRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateCategory(Guid MediumId, Category category)
      {
         category.MediumId = MediumId;
         Create(category);
      }

      public async Task<IEnumerable<Category>> GetMediumCategoriesAsync
         (Guid mediumId, bool trackChanges)
      {
         return
            await FindByCondition(c => c.MediumId.Equals(mediumId),
            trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
      }

      public async Task<Category?> GetMediumCategoryAsync
         (Guid MediumId, Guid categoryId, bool trackChanges)
      {
         return
            await FindByCondition(c => c.CategoryId.Equals(categoryId)
            && c.MediumId.Equals(MediumId)
            , trackChanges)
            .SingleOrDefaultAsync();
      }

      public async Task<Category?> GetCategoryAsync
         (Guid categoryId, bool trackChanges)
      {
         var category =
            await FindByCondition(c => c.CategoryId.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

         return category;
      }

      public void RemoveCategory(Category category)
      {
         Delete(category);
      }
   }

}
