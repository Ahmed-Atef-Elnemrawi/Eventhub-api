using EventHub.EventManagement.API.Utility.EventUtility;
using EventHub.EventManagement.API.Utility.OrganizationUtility;
using EventHub.EventManagement.API.Utility.ProducerUtitilty;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Links;
using EventHub.EventManagement.Application.Contracts.Service.DataShaperService;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.DTOs.CategoryDto;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.DTOs.MediumDto;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;

namespace EventHub.EventManagement.API.Utility
{
   public class EntitiesLinkGeneratorManager : IEntitiesLinkGeneratorManager
   {
      private readonly Lazy<IEntityLinks<EventDto>> _eventLinks;
      private readonly Lazy<IEntityLinks<EventDto>> _organizationEventLinks;
      private readonly Lazy<IEntityLinks<EventDto>> _producerEventLinks;
      private readonly Lazy<IEntityLinks<OrganizationDto>> _organizationLinks;
      private readonly Lazy<IEntityLinks<ProducerDto>> _producerLinks;
      private readonly Lazy<IEntityLinks<CategoryDto>> _categoryLinks;
      private readonly Lazy<IEntityLinks<MediumDto>> _mediumLinks;
      private readonly Lazy<IEntityLinks<FollowerDto>> _organizationFollowersLinks;
      private readonly Lazy<IEntityLinks<FollowerDto>> _producerFollowersLinks;
      private readonly Lazy<IEntityLinks<AttendantDto>> _producerEventAttendantLinks;
      private readonly Lazy<IEntityLinks<AttendantDto>> _organizationEventAttendantLinks;
      private readonly Lazy<IEntityLinks<SpeakerDto>> _organizationSpeakerLinks;
      private readonly Lazy<IEntityLinks<SpeakerDto>> _organizationEventSpeakerLinks;
      private readonly Lazy<IEntityLinks<EventDto>> _categoryOrganizationEventLinks;
      private readonly Lazy<IEntityLinks<EventDto>> _categoryProducerEventLinks;



      public EntitiesLinkGeneratorManager(LinkGenerator linkGenerator, IDataShaperManager dataShaper)
      {
         _eventLinks = new Lazy<IEntityLinks<EventDto>>(() =>
         new EventLinks(linkGenerator));

         _categoryOrganizationEventLinks = new Lazy<IEntityLinks<EventDto>>(() =>
         new CategoryOrganizationEventLink(linkGenerator));

         _categoryProducerEventLinks = new Lazy<IEntityLinks<EventDto>>(() =>
       new CategoryProducerEventLinks(linkGenerator));

         _organizationEventLinks = new Lazy<IEntityLinks<EventDto>>(() =>
          new OrganizationEventLinks(linkGenerator));

         _producerEventLinks = new Lazy<IEntityLinks<EventDto>>(() =>
          new ProducerEventLinkes(linkGenerator));

         _producerLinks = new Lazy<IEntityLinks<ProducerDto>>(() =>
          new ProducerLinks(linkGenerator));

         _organizationLinks = new Lazy<IEntityLinks<OrganizationDto>>(() =>
          new OrganizationLinks(linkGenerator));

         _categoryLinks = new Lazy<IEntityLinks<CategoryDto>>(() =>
          new CategoryLinks(linkGenerator));

         _mediumLinks = new Lazy<IEntityLinks<MediumDto>>(() =>
          new MediumLinks(linkGenerator));

         _producerEventAttendantLinks = new Lazy<IEntityLinks<AttendantDto>>(() =>
         new ProducerEventAttendantLinks(linkGenerator));

         _organizationEventAttendantLinks = new Lazy<IEntityLinks<AttendantDto>>(() =>
         new OrganizationEventAttendantLinks(linkGenerator));

         _producerFollowersLinks = new Lazy<IEntityLinks<FollowerDto>>(() =>
          new ProducerFollowerLinks(linkGenerator));

         _organizationFollowersLinks = new Lazy<IEntityLinks<FollowerDto>>(() =>
          new OrganizationFollowerLinks(linkGenerator));

         _organizationSpeakerLinks = new Lazy<IEntityLinks<SpeakerDto>>(() =>
         new SpeakerLinks(linkGenerator));

         _organizationEventSpeakerLinks = new Lazy<IEntityLinks<SpeakerDto>>(() =>
         new EventSpeakerLinks(linkGenerator));
      }

      public IEntityLinks<EventDto> CategoryOrganizationEventLinks =>
         _categoryOrganizationEventLinks.Value;

      public IEntityLinks<EventDto> CategoryProducerEventLinks =>
         _categoryProducerEventLinks.Value;

      public IEntityLinks<EventDto> EventLinks =>
         _eventLinks.Value;
      public IEntityLinks<EventDto> ProducerEventLinks =>
         _producerEventLinks.Value;

      public IEntityLinks<OrganizationDto> OrganizationLinks =>
         _organizationLinks.Value;

      public IEntityLinks<ProducerDto> ProducerLinks =>
         _producerLinks.Value;

      public IEntityLinks<AttendantDto> ProducerEventAttendantLinks =>
         _producerEventAttendantLinks.Value;

      public IEntityLinks<AttendantDto> OrganizationEventAttendantLinks =>
         _organizationEventAttendantLinks.Value;

      public IEntityLinks<CategoryDto> CategoryLinks =>
         _categoryLinks.Value;

      public IEntityLinks<FollowerDto> OrganizationFollowerLinks =>
         _organizationFollowersLinks.Value;

      public IEntityLinks<FollowerDto> ProducerFollowerLinks =>
         _producerFollowersLinks.Value;

      public IEntityLinks<MediumDto> MediumLinks =>
         _mediumLinks.Value;

      public IEntityLinks<SpeakerDto> OrganizationSpeakerLinks =>
         _organizationSpeakerLinks.Value;

      public IEntityLinks<SpeakerDto> OrganizationEventSpeakerLinks =>
         _organizationEventSpeakerLinks.Value;

      public IEntityLinks<EventDto> OrganizationEventLinks =>
         _organizationEventLinks.Value;


   }
}
