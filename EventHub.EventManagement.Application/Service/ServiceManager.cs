using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.Contracts.Service.DataShaperService;
using EventHub.EventManagement.Application.Contracts.Service.EventServices;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.Contracts.Service.ProducerServices;
using EventHub.EventManagement.Application.Service.EventServices;
using EventHub.EventManagement.Application.Service.OrganizationServices;
using EventHub.EventManagement.Application.Service.ProducerServices;
using EventHub.EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EventHub.EventManagement.Application.Service
{
   internal sealed class ServiceManager : IServiceManager
   {
      private readonly Lazy<IAuthenticationService> _authenticationService;

      private readonly Lazy<ICategoryService> _categoryService;
      private readonly Lazy<IEventService> _eventService;
      private readonly Lazy<IMediumService> _mediumService;

      private readonly Lazy<IOrganizationEventService> _organizationEventService;
      private readonly Lazy<IOrganizationFollowersService> _organizationFollowersService;
      private readonly Lazy<IOrganizationService> _organizationService;
      private readonly Lazy<IOrganizationEventSpeakerService> _organizationEventSpeakerService;
      private readonly Lazy<IOrganizationSpeakerService> _organizationSpeakerService;
      private readonly Lazy<IOrganizationEventAttendantsService> _organizationEventAttendantsService;

      private readonly Lazy<IProducerEventService> _producerEventService;
      private readonly Lazy<IProducerFollowersService> _producerFollowersService;
      private readonly Lazy<IProducerEventAttendantsService> _producerEventAttendantsService;
      private readonly Lazy<IProducerService> _producerService;

      public ServiceManager
         (
         IRepositoryManager repository,
         ILoggerManager logger,
         IMapper mapper,
         IDataShaperManager dataShaper,
         IEntitiesLinkGeneratorManager entitiesLinkGenerator,
         IConfiguration configuration,
         UserManager<User> userManager)
      {

         _authenticationService =
            new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(userManager, logger, mapper, configuration));

         _organizationService =
            new Lazy<IOrganizationService>(() =>
            new OrganizationService(repository, logger, mapper, entitiesLinkGenerator));

         _organizationEventService =
            new Lazy<IOrganizationEventService>(() =>
            new OrganizationEventService(repository, logger, mapper, entitiesLinkGenerator));

         _organizationFollowersService =
            new Lazy<IOrganizationFollowersService>(() =>
            new OrganizationFollowersService(repository, logger, mapper, entitiesLinkGenerator));

         _organizationEventSpeakerService =
            new Lazy<IOrganizationEventSpeakerService>(() =>
            new OrganizationEventSpeakerService(repository, logger, mapper, entitiesLinkGenerator));

         _organizationEventAttendantsService =
            new Lazy<IOrganizationEventAttendantsService>(() =>
            new OrganizationEventAttendantsService(repository, logger, mapper, entitiesLinkGenerator));

         _organizationSpeakerService =
            new Lazy<IOrganizationSpeakerService>(() =>
            new OrganizationSpeakerService(repository, logger, mapper, entitiesLinkGenerator));

         _producerService =
            new Lazy<IProducerService>(() =>
            new ProducerService(repository, logger, mapper, entitiesLinkGenerator));

         _categoryService =
            new Lazy<ICategoryService>(() =>
            new CategoryService(repository, logger, mapper, entitiesLinkGenerator));

         _producerEventService =
            new Lazy<IProducerEventService>(() =>
            new ProducerEventService(repository, logger, mapper, entitiesLinkGenerator));

         _mediumService =
            new Lazy<IMediumService>(() =>
            new MediumService(repository, logger, mapper, entitiesLinkGenerator));

         _producerFollowersService =
            new Lazy<IProducerFollowersService>(() =>
            new ProducerFollowersService(repository, logger, mapper, entitiesLinkGenerator));

         _producerEventAttendantsService =
           new Lazy<IProducerEventAttendantsService>(() =>
           new ProducerEventAttendantsService(repository, logger, mapper, entitiesLinkGenerator));


         _eventService =
            new Lazy<IEventService>(() =>
            new EventService(repository, logger, mapper, entitiesLinkGenerator, dataShaper));
      }


      public IAuthenticationService AuthenticationService =>
         _authenticationService.Value;

      public IOrganizationService OrganizationService =>
         _organizationService.Value;

      public IOrganizationEventService OrganizationEventService =>
         _organizationEventService.Value;

      public IOrganizationFollowersService OrganizationFollowersService =>
         _organizationFollowersService.Value;

      public IProducerService ProducerService =>
         _producerService.Value;

      public ICategoryService CategoryService =>
         _categoryService.Value;

      public IProducerEventService ProducerEventService =>
         _producerEventService.Value;

      public IMediumService MediumService =>
         _mediumService.Value;

      public IProducerFollowersService ProducerFollowersService =>
         _producerFollowersService.Value;

      public IEventService EventService =>
         _eventService.Value;

      public IOrganizationEventSpeakerService OrganizationEventSpeakerService =>
         _organizationEventSpeakerService.Value;

      public IOrganizationSpeakerService OrganizationSpeakerService =>
         _organizationSpeakerService.Value;

      public IOrganizationEventAttendantsService OrganizationEventAttendantsService =>
         _organizationEventAttendantsService.Value;

      public IProducerEventAttendantsService ProducerEventAttendantsService =>
         _producerEventAttendantsService.Value;
   }

}
