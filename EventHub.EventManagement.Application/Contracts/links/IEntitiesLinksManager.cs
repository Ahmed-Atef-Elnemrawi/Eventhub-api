
using EventHub.EventManagement.Application.Contracts.Links;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.DTOs.CategoryDto;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.DTOs.MediumDto;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;

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
