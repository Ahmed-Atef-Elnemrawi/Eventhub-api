using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service.ProducerServices;
using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.Service.ProducerServices
{
   internal sealed class ProducerFollowersService : IProducerFollowersService
   {
      private readonly IRepositoryManager _repository;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly IEntitiesLinkGeneratorManager _entitiesLinkGenerator;

      public ProducerFollowersService
         (
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

      public async Task<FollowerDto> CreateFollowerAsync(Guid producerId,
         FollowerForCreationDto follower, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var followerEntity = _mapper
            .Map<Follower>(follower);

         _repository
            .ProducerFollowersRepository
            .CreateFollower(producerId, followerEntity);

         await _repository.SaveAsync();

         var followerToReturn = _mapper
            .Map<FollowerDto>(followerEntity);

         return followerToReturn;
      }

      public async Task<LinkResponse> GetFollowerAsync
         (Guid producerId, Guid followerId, FollowerLinkParams linkParams, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var follower =
            await GetFollowerAndCheckIfItExists(producerId, followerId, trackChanges);

         var followerDto = _mapper
            .Map<FollowerDto>(follower);

         var linkResponse = _entitiesLinkGenerator.ProducerFollowerLinks
            .TryGetEntityLinks(followerDto,
                               linkParams.followerParams.Fields!,
                               linkParams.HttpContext,
                               producerId);
         return linkResponse;
      }

      public async Task<(LinkResponse linkResponse, MetaData metaData)>
         GetAllFollowersAsync(Guid producerId, FollowerLinkParams linkParams, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var followersWithMetaData = await _repository
            .ProducerFollowersRepository
            .GetAllFollowersAsync(producerId, linkParams.followerParams, trackChanges);

         var followersDto = _mapper
            .Map<IEnumerable<FollowerDto>>(followersWithMetaData);

         var linkResposne = _entitiesLinkGenerator.ProducerFollowerLinks
            .TryGetEntitiesLinks(followersDto,
                                 linkParams.followerParams.Fields!,
                                 linkParams.HttpContext,
                                 producerId);

         return (linkResposne, metaData: followersWithMetaData.MetaData);
      }


      public async Task RemoveFollowerAsync(Guid producerId, Guid followerId, bool trackChanges)
      {
         await CheckIfProducerExists(producerId, trackChanges);

         var follower =
            await GetFollowerAndCheckIfItExists(producerId, followerId, trackChanges);

         _repository
            .ProducerFollowersRepository
            .RemoveFollower(follower);

         await _repository.SaveAsync();
      }

      private async Task CheckIfProducerExists(Guid producerId, bool trackChanges)
      {
         var producer = await _repository
            .ProducerRepository
            .GetProducerAsync(producerId, trackChanges);

         if (producer is null)
            throw new ProducerNotFound("id", producerId);
      }

      private async Task<Follower> GetFollowerAndCheckIfItExists(Guid producerId, Guid followerId,
         bool trackChanges)
      {
         var follower = await _repository
           .ProducerFollowersRepository
           .GetFollowerAsync(producerId, followerId, trackChanges);

         if (follower is null)
            throw new FollowerNotFound("id", followerId);

         return follower;

      }
   }
}