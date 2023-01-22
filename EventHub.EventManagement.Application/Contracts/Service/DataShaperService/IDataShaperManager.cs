using EventHub.EventManagement.Application.Contracts.Service.DataShapingService;
using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.DTOs.EventDtos;
using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.DTOs.OrganizationDtos;
using EventHub.EventManagement.Application.DTOs.ProducerDtos;
using EventHub.EventManagement.Application.DTOs.SpeakerDtos;

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
