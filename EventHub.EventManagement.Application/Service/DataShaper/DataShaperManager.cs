using EventHub.EventManagement.Application.Contracts.Service.DataShaperService;
using EventHub.EventManagement.Application.Contracts.Service.DataShapingService;
using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.DTOs.EventDtos;
using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.DTOs.OrganizationDtos;
using EventHub.EventManagement.Application.DTOs.ProducerDtos;
using EventHub.EventManagement.Application.DTOs.SpeakerDtos;

namespace EventHub.EventManagement.Application.Service.DataShaper
{
   internal sealed class DataShaperManager : IDataShaperManager
   {
      private readonly Lazy<IDataShaper<EventDto>> _eventDataShaper;
      private readonly Lazy<IDataShaper<AttendantDto>> _attendantDataShaper;
      private readonly Lazy<IDataShaper<ProducerDto>> _producerDataShaper;
      private readonly Lazy<IDataShaper<OrganizationDto>> _organizationDataShaper;
      private readonly Lazy<IDataShaper<FollowerDto>> _followerDataShaper;
      private readonly Lazy<IDataShaper<SpeakerDto>> _speakerDataShaper;


      public DataShaperManager()
      {
         _attendantDataShaper =
            new Lazy<IDataShaper<AttendantDto>>(() =>
            new DataShaper<AttendantDto>());

         _eventDataShaper =
            new Lazy<IDataShaper<EventDto>>(() =>
            new DataShaper<EventDto>());

         _producerDataShaper =
            new Lazy<IDataShaper<ProducerDto>>(() =>
            new DataShaper<ProducerDto>());

         _organizationDataShaper =
            new Lazy<IDataShaper<OrganizationDto>>(() =>
            new DataShaper<OrganizationDto>());

         _followerDataShaper =
            new Lazy<IDataShaper<FollowerDto>>(() =>
            new DataShaper<FollowerDto>());

         _speakerDataShaper =
            new Lazy<IDataShaper<SpeakerDto>>(() =>
            new DataShaper<SpeakerDto>());
      }


      public IDataShaper<EventDto> EventDataShaper =>
         _eventDataShaper.Value;

      public IDataShaper<AttendantDto> AttendantDataShaper =>
         _attendantDataShaper.Value;

      public IDataShaper<ProducerDto> ProducerDataShaper =>
         _producerDataShaper.Value;

      public IDataShaper<OrganizationDto> OrganizationDataShaper =>
         _organizationDataShaper.Value;

      public IDataShaper<FollowerDto> FollowerDataShaper =>
         _followerDataShaper.Value;

      public IDataShaper<SpeakerDto> SpeakerDataShaper =>
         _speakerDataShaper.Value;

   }
}
