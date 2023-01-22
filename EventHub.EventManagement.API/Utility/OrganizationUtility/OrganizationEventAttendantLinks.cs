using EventHub.EventManagement.Application.DTOs.AttendantDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.OrganizationUtility
{
   public class OrganizationEventAttendantLinks : EntityLinkes<AttendantDto>
   {
      public OrganizationEventAttendantLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields = "")
      {
         var eventId = parentId;
         var organizationId = parentParentId;

         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetOrganizationEventAttendant",
               values: new {organizationId, eventId, id, fields}),
               rel:"self", method:"GET"),


            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveOrganizationEventAttendant",
               values: new {organizationId, eventId, id}),
               rel:"delete-attendant", method:"DELETE"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {


         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllOrganizationEventAttendants",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
