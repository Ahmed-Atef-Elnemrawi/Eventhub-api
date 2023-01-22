
using EventHub.EventManagement.Application.Contracts.Links;
using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.DTOs.CategoryDtos;
using EventHub.EventManagement.Application.DTOs.EventDtos;
using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.DTOs.MediumDtos;
using EventHub.EventManagement.Application.DTOs.OrganizationDtos;
using EventHub.EventManagement.Application.DTOs.ProducerDtos;
using EventHub.EventManagement.Application.DTOs.SpeakerDtos;

namespace EventHub.EventManagement.Application.Contracts.links
{
   public interface IEntitiesLinkGeneratorManager
   {
      IEntityLinks<AttendantDto> OrganizationEventAttendantLinks { get; }
      IEntityLinks<AttendantDto> ProducerEventAttendantLinks { get; }

      IEntityLinks<CategoryDto> CategoryLinks { get; }
      IEntityLinks<EventDto> CategoryOrganizationEventLinks { get; }
      IEntityLinks<EventDto> CategoryProducerEventLinks { get; }

      IEntityLinks<EventDto> EventLinks { get; }
      IEntityLinks<EventDto> OrganizationEventLinks { get; }
      IEntityLinks<EventDto> ProducerEventLinks { get; }

      IEntityLinks<FollowerDto> OrganizationFollowerLinks { get; }
      IEntityLinks<FollowerDto> ProducerFollowerLinks { get; }

      IEntityLinks<MediumDto> MediumLinks { get; }

      IEntityLinks<OrganizationDto> OrganizationLinks { get; }
      IEntityLinks<ProducerDto> ProducerLinks { get; }

      IEntityLinks<SpeakerDto> OrganizationEventSpeakerLinks { get; }
      IEntityLinks<SpeakerDto> OrganizationSpeakerLinks { get; }
   }
}
