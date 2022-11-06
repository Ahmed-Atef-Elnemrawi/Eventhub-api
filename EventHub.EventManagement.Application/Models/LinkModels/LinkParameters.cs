using EventHub.EventManagement.Application.RequestFeatures.Params;
using Microsoft.AspNetCore.Http;

namespace EventHub.EventManagement.Application.Models.LinkModels
{
   public record EventLinkParams(EventParams eventParams, HttpContext HttpContext);
   public record ProducerLinkParams(ProducerParams producerParams, HttpContext HttpContext);
   public record OrganizationLinkParams(OrganizationParams organizationParams, HttpContext HttpContext);
   public record AttendantLinkParams(AttendantParams attendantParams, HttpContext HttpContext);
   public record FollowerLinkParams(FollowerParams followerParams, HttpContext HttpContext);
   public record SpeakerLinkParams(SpeakerParams speakerParams, HttpContext HttpContext);
   public record CategoryLinkParams(HttpContext HttpContext);
   public record MediumLinkParams(HttpContext HttpContext);

}
