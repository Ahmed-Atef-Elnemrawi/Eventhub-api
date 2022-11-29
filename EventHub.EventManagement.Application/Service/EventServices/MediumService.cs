using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.EventServices;
using EventHub.EventManagement.Application.DTOs.MediumDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.Application.Service.EventServices
{
   internal sealed class MediumService : IMediumService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public MediumService(
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
      public async Task<LinkResponse> GetMediumAsync
         (Guid mediumId, MediumLinkParams linkParams, bool trackChanges)
      {
         var mediumEntity = await _repository
            .MediumRepository
            .GetMediumAsync(mediumId, trackChanges);

         if (mediumEntity is null)
            throw new MediumNotFound("id", mediumId);

         var mediumToDto = _mapper
            .Map<MediumDto>(mediumEntity);

         var linkResponse = _entitiesLinkGenerator.MediumLinks
            .TryGetEntityLinks(mediumToDto, "", linkParams.HttpContext);

         return linkResponse;
      }
      public async Task<LinkResponse> GetAllMediumsAsync(MediumLinkParams linkParams, bool trackChanges)
      {
         var mediumsEntities = await _repository
            .MediumRepository
            .GetAllMediumsAsync(trackChanges);

         var mediumsDto = _mapper
            .Map<IEnumerable<MediumDto>>(mediumsEntities);

         var linkResponse = _entitiesLinkGenerator.MediumLinks
            .TryGetEntitiesLinks(mediumsDto, "", linkParams.HttpContext);

         return linkResponse;
      }
   }
}
