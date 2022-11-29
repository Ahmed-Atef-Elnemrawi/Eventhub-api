using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.Service.OrganizationServices
{
   internal sealed class OrganizationFollowersService : IOrganizationFollowersService
   {

      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public OrganizationFollowersService
         (
         IRepositoryManager repository,
         ILoggerManager logger,
         IMapper mapper,
         IEntitiesLinkGeneratorManager entitiesLinkGeneratorManager)
      {
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         _entitiesLinkGenerator = entitiesLinkGeneratorManager ?? throw new ArgumentNullException(nameof(entitiesLinkGeneratorManager));
      }

      public async Task<FollowerDto> CreateFollowerAsync
         (Guid organizationId, FollowerForCreationDto follower, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var followerEntity =
            _mapper.Map<Follower>(follower);

         _repository
            .OrganizationFollowersRepository
            .CreateFollower(organizationId, followerEntity);

         await _repository.SaveAsync();

         var followerToReturn =
            _mapper.Map<FollowerDto>(followerEntity);

         return followerToReturn;
      }

      public async Task<LinkResponse> GetFollowerAsync
         (Guid organizationId, Guid followerId, FollowerLinkParams linkParams, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var follower = await
            GetFollowerAndCheckIfItExists(organizationId, followerId, trackChanges);

         var followerDto =
            _mapper.Map<FollowerDto>(follower);

         var linkResponse = _entitiesLinkGenerator.OrganizationFollowerLinks
            .TryGetEntityLinks(followerDto,
                               linkParams.followerParams.Fields!,
                               linkParams.HttpContext,
                               organizationId);

         return linkResponse;
      }

      public async Task<(LinkResponse link, MetaData metaData)>
         GetAllFollowersAsync(Guid organizationId, FollowerLinkParams linkParams, bool trackChanges)
      {

         await CheckIfOrganizationExists(organizationId, trackChanges);

         var followersWithMetaData = await _repository
            .OrganizationFollowersRepository
            .GetAllFollowersAsync(organizationId, linkParams.followerParams, trackChanges);

         var followersDto =
            _mapper.Map<IEnumerable<FollowerDto>>(followersWithMetaData);

         var link = _entitiesLinkGenerator.OrganizationFollowerLinks
             .TryGetEntitiesLinks(followersDto,
                                  linkParams.followerParams.Fields!,
                                  linkParams.HttpContext,
                                  organizationId);

         return (link, metaData: followersWithMetaData.MetaData);
      }


      public async Task RemoveFollowerAsync(Guid organizationId, Guid followerId, bool trackChanges)
      {
         await CheckIfOrganizationExists(organizationId, trackChanges);

         var follower = await
            GetFollowerAndCheckIfItExists(organizationId, followerId, trackChanges);

         _repository
            .OrganizationFollowersRepository
            .RemoveFollower(follower);

         await _repository.SaveAsync();
      }

      private async Task CheckIfOrganizationExists(Guid organizationId, bool trackChanges)
      {
         var organizationEntity = await _repository
            .OrganizationRepository
            .GetOrganizationAsync(organizationId, trackChanges);

         if (organizationEntity is null)
            throw new OrganizationNotFound("id", organizationId);
      }

      private async Task<Follower> GetFollowerAndCheckIfItExists(Guid organizationId,
         Guid followerId, bool trackChanges)
      {
         var follower = await _repository
           .OrganizationFollowersRepository
           .GetFollowerAsync(organizationId, followerId, trackChanges);

         if (follower is null)
            throw new FollowerNotFound("id", followerId);

         return follower;
      }
   }
}
