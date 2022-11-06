using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.EventUtility
{
   public class CategoryOrganizationEventLink : EntityLinkes<EventDto>
   {
      public CategoryOrganizationEventLink(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields)
      {
         var organizationId = parentId;
         var eventId = id;
         var links = new List<Link>
        {
           new Link(_linkGenerator.GetUriByName(httpContext,"GetOrganization",
           new{id = organizationId}),
           rel:"organization", method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext,"CreateOrganizationFollower",
           new{organizationId}),
           rel:"create-organization-follower", method:"POST"),

           new Link(_linkGenerator.GetUriByName(httpContext,"CreateOrganizationEventAttendant",
           new{organizationId, eventId}),
           rel:"create-event-attendant", method:"POST"),


        };

         return links;
      }
   }
}
