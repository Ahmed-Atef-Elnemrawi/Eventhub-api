using EventHub.EventManagement.Application.Contracts.Service.DataShapingService;
using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.DTOs.FollowerDto;
using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.DTOs.SpeakerDto;

namespace EventHub.EventManagement.Application.Contracts.Service.DataShaperService
{
   public interface IDataShaperManager
   {
      IDataShaper<EventDto> EventDataShaper { get; }
      IDataShaper<AttendantDto> AttendantDataShaper { get; }
      IDataShaper<ProducerDto> ProducerDataShaper { get; }
      IDataShaper<OrganizationDto> OrganizationDataShaper { get; }
      IDataShaper<FollowerDto> FollowerDataShaper { get; }
      IDataShaper<SpeakerDto> SpeakerDataShaper { get; }
   }
}
