using EventHub.EventManagement.Application.DTOs.EventDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.OrganizationUtility;

public class OrganizationEventLinks : EntityLinkes<EventDto>
{
   public OrganizationEventLinks(LinkGenerator linkGenerator) : base(linkGenerator)
   {
   }

   internal override List<Link> GenerateEntityLinks(
      HttpContext httpContext,
      Guid id,
      Guid? parentId,
      Guid? parentParentId,
      string fields = "")
   {
      var eventId = id;
      var organizationId = parentId;

      var links = new List<Link>
      {
         new Link(_linkGenerator.GetUriByAction(httpContext, "GetOrganizationEvent",
         values: new {organizationId,id, fields}),
         rel:"self", method:"GET"),

         new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateOrganizationEvent",
         values: new {organizationId, id}),
         rel:"update-event", method:"PUT"),

         new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveOrganizationEvent",
         values: new {organizationId, id}),
         rel:"delete-event", method:"DELETE"),

         new Link(_linkGenerator.GetUriByName(httpContext, "GetOrganizationEventSpeakers",
         new {organizationId, eventId}),
         rel:"get-organization-event-speakers", method:"GET"),

          new Link(_linkGenerator.GetUriByName(httpContext, "CreateOrganizationEventSpeaker",
          new {organizationId, eventId}),
         rel:"Create-organization-event-speaker", method:"POST"),

         new Link(_linkGenerator.GetUriByName(httpContext, "GetOrganizationEventAttendants",
         new {organizationId, eventId}),
         rel:"get-organization-event-attendants", method:"GET"),

          new Link(_linkGenerator.GetUriByName(httpContext, "CreateOrganizationEventAttendant",
          new { organizationId, eventId }),
         rel:"Create-organization-event-attendant", method:"POST"),



      };
      return links;
   }

   internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
      (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
   {
      var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllOrganizationEvents",
         values: new { }),
         rel: "self", method: "GET");

      entitiesCollection.Links!.Add(link);

      return entitiesCollection;
   }
}
