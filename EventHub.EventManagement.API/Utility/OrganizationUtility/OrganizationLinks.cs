using EventHub.EventManagement.Application.DTOs.OrganizationDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.OrganizationUtility
{
   public class OrganizationLinks : EntityLinkes<OrganizationDto>
   {
      public OrganizationLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {

      }

      internal override List<Link> GenerateEntityLinks(
         HttpContext httpContext,
         Guid id,
         Guid? parentId,
         Guid? parentParentId,
         string fields = "")
      {
         var organizationId = id;
         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetOrganization", values: new {id, fields}),
            rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateOrganization", values: new {id}),
            rel:"update-organization", method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveOrganization", values: new {id}),
            rel:"delete-organization", method:"DELETE"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetOrganizationEvents",
            new{organizationId}),
             rel:"get-organization-events",method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext, "CreateOrganizationEvent",
            new{organizationId}),
             rel:"create-organization-event",method:"POST"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetOrganizationSpeakers",
            new{organizationId}),
             rel:"get-organization-speakers",method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext, "CreateOrganizationSpeaker",
            new{organizationId}),
             rel:"create-organization-speaker",method:"POST"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetOrganizationFollowers",
            new{organizationId}),
             rel:"get-organization-followers",method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext, "CreateOrganizationFollower",
            new{organizationId}),
             rel:"create-organization-follower",method:"POST")

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllOrganizations", values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
