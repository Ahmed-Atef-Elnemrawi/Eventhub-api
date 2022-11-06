using AutoMapper;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Logging;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationService : IOrganizationService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationService
         (IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IEntitiesLinkGeneratorManager entitiesLinkGenerator)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = entitiesLinkGenerator;
      }

      public async Task<OrganizationDto> CreateOrganizationAsync
         (OrganizationForCreationDto organizationForCreationDto)
      {
         var organization = _mapper
            .Map<Organization>(organizationForCreationDto);

         _repository
            .OrganizationRepository
            .CreateOrganization(organization);

         await _repository.SaveAsync();

         var organizationToReturn = _mapper
            .Map<OrganizationDto>(organization);

         return organizationToReturn;
      }

      public async Task<LinkResponse> GetOrganizationAsync
         (Guid organizationId, OrganizationLinkParams linkParams, bool trackChanges)
      {
         var organization = await
            GetOrganizationAndCheckIfItExists(organizationId, trackChanges);

         var organizationDto = _mapper
            .Map<OrganizationDto>(organization);

         var linkResponse = _entitiesLinkGenerator.OrganizationLinks
            .TryGetEntityLinks(organizationDto, linkParams.organizationParams.Fields!, linkParams.HttpContext);

         return linkResponse;
      }


      public async Task<(LinkResponse link, MetaData metaData)>
         GetAllOrganizationsAsync(OrganizationLinkParams organizationLinkParams, bool trackChanges)
      {
         var organizationsWithMetaData = await _repository
            .OrganizationRepository
            .GetAllOrganizationsAsync(organizationLinkParams.organizationParams, trackChanges);

         var organizationsDto = _mapper
            .Map<IEnumerable<OrganizationDto>>(organizationsWithMetaData);

         var linkResponse =
             _entitiesLinkGenerator.OrganizationLinks.TryGetEntitiesLinks(organizationsDto,
               organizationLinkParams.organizationParams.Fields!, organizationLinkParams.HttpContext);

         return (link: linkResponse, metaData: organizationsWithMetaData.MetaData);
      }

      public async Task RemoveOrganizationAsync(Guid organizationId, bool trackChanges)
      {
         var organization = await
            GetOrganizationAndCheckIfItExists(organizationId, trackChanges);

         _repository
            .OrganizationRepository
            .RemoveOrganization(organization);

         await _repository.SaveAsync();
      }

      public async Task UpdateOrganizationAsync(Guid organizationId,
         OrganizationForUpdateDto organizationForUpdate, bool trackChanges)
      {
         var organization = await
            GetOrganizationAndCheckIfItExists(organizationId, trackChanges);

         _mapper.Map(organizationForUpdate, organization);

         await _repository.SaveAsync();
      }


      private async Task<Organization> GetOrganizationAndCheckIfItExists(Guid organizationId, bool trackChanges)
      {
         var organization = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges);

         if (organization is null)
            throw new OrganizationNotFoundException("id", organizationId);

         return organization;
      }
   }
}
