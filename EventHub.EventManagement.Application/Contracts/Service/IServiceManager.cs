using EventHub.EventManagement.Application.Contracts.Service.EventServices;
using EventHub.EventManagement.Application.Contracts.Service.OrganizationServices;
using EventHub.EventManagement.Application.Contracts.Service.ProducerServices;

namespace EventHub.EventManagement.Application.Contracts.Service
{
   public interface IServiceManager
   {
      ICategoryService CategoryService { get; }
      IEventService EventService { get; }
      IMediumService MediumService { get; }

      IOrganizationEventService OrganizationEventService { get; }
      IOrganizationEventSpeakerService OrganizationEventSpeakerService { get; }
      IOrganizationFollowersService OrganizationFollowersService { get; }
      IOrganizationService OrganizationService { get; }
      IOrganizationSpeakerService OrganizationSpeakerService { get; }
      IOrganizationEventAttendantsService OrganizationEventAttendantsService { get; }

      IProducerEventService ProducerEventService { get; }
      IProducerFollowersService ProducerFollowersService { get; }
      IProducerEventAttendantsService ProducerEventAttendantsService { get; }
      IProducerService ProducerService { get; }
   }
}
