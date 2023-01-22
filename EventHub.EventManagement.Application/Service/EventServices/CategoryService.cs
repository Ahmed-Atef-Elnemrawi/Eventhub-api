using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.EventServices;
using EventHub.EventManagement.Application.DTOs.CategoryDtos;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Service.EventServices
{
   internal sealed class CategoryService : ICategoryService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public CategoryService(
         IRepositoryManager repository,
         ILoggerManager logger,
         IMapper mapper,
         IEntitiesLinkGeneratorManager entitiesLinkGenerator)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = entitiesLinkGenerator ?? throw new ArgumentNullException(nameof(entitiesLinkGenerator));
      }

      public async Task<LinkResponse> GetAllMediumCategoriesAsync
         (Guid mediumId, CategoryLinkParams linkParams, bool trackChanges)
      {
         await CheckIfMediumExists(mediumId, trackChanges);

         var categories = await _repository
            .CategoryRepository
            .GetMediumCategoriesAsync(mediumId, trackChanges);

         var categoriesDto = _mapper
            .Map<IEnumerable<CategoryDto>>(categories);

         var linkResponse = _entitiesLinkGenerator.CategoryLinks
            .TryGetEntitiesLinks(categoriesDto, "", linkParams.HttpContext, mediumId);

         return linkResponse;
      }

      public async Task<LinkResponse> GetMediumCategoryAsync
         (Guid mediumId, Guid categoryId, CategoryLinkParams linkParams, bool trackChanges)
      {
         var category = await
            GetCategoryAndCheckIfItExist(mediumId, categoryId, trackChanges);

         var categoryDto = _mapper
            .Map<CategoryDto>(category);

         var linkResponse = _entitiesLinkGenerator.CategoryLinks
            .TryGetEntityLinks(categoryDto, "", linkParams.HttpContext, mediumId);

         return linkResponse;
      }


      public async Task<CategoryDto> CreateCategoryAsync(Guid mediumId,
         CategoryForCreationDto category, bool trackChanges)
      {

         await CheckIfMediumExists(mediumId, trackChanges);

         var categoryEntity = _mapper
            .Map<Category>(category);

         _repository
            .CategoryRepository
            .CreateCategory(mediumId, categoryEntity);

         await _repository.SaveAsync();

         var categoryToReturn = _mapper
            .Map<CategoryDto>(categoryEntity);

         return categoryToReturn;
      }

      public async Task RemoveCategoryAsync(Guid mediumId, Guid categoryId, bool trackChanges)
      {
         await CheckIfMediumExists(mediumId, trackChanges);

         var category = await
            GetCategoryAndCheckIfItExist(mediumId, categoryId, trackChanges);

         _repository
            .CategoryRepository
            .RemoveCategory(category);

         await _repository.SaveAsync();
      }

      public async Task UpdateCategoryAsync(Guid mediumId, Guid categoryId, CategoryForUpdateDto
         categoryForUpdate, bool trackChanges)
      {

         await CheckIfMediumExists(mediumId, false);

         var category =
             await GetCategoryAndCheckIfItExist(mediumId, categoryId, trackChanges);

         _mapper.Map(categoryForUpdate, category);

         await _repository.SaveAsync();
      }


      private async Task CheckIfMediumExists(Guid mediumId, bool trackChanges)
      {
         var medium = await _repository
            .MediumRepository
            .GetMediumAsync(mediumId, trackChanges);

         if (medium is null)
            throw new MediumNotFound("id", mediumId);
      }
      private async Task<Category> GetCategoryAndCheckIfItExist(Guid mediumId, Guid categoryId, bool trackChanges)
      {
         var category = await _repository
           .CategoryRepository
           .GetMediumCategoryAsync(mediumId, categoryId, trackChanges);

         if (category is null)
            throw new CategoryNotFound("id", categoryId);

         return category;
      }
   }
}
