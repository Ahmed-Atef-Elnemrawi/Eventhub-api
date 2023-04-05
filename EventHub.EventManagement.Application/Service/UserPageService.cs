using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs;
using EventHub.EventManagement.Application.Exceptions;

namespace EventHub.EventManagement.Application.Service
{
   public sealed class UserPageService : IUserPageService
   {
      private readonly IRepositoryManager _repository;
      private readonly IMapper _mapper;

      public UserPageService(IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
      {
         _repository = repository;
         _mapper = mapper;
      }
      public async Task<UserPageDto> GetUserPageAsync(Guid userPageId, bool trackChanges)
      {
         var userPage = await _repository.UserPageRepository.GetUserPageAsync(userPageId, trackChanges);
         if (userPage is null)
            throw new UserPageNotFound("id", userPageId);

         var userPageToReturn = _mapper.Map<UserPageDto>(userPage);

         return userPageToReturn;
      }
   }
}
