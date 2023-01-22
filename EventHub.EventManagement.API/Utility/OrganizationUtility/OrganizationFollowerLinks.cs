using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.OrganizationUtility
{
   public class OrganizationFollowerLinks : EntityLinkes<FollowerDto>
   {
      public OrganizationFollowerLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields = "")
      {
         var organizationId = parentId;
         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetOrganizationFollower",
               values: new { organizationId,id, fields}),
               rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateOrganizationFollower",
               values: new { organizationId,id}),
               rel:"update-follower", method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveOrganizationFollower",
               values: new { organizationId,id}),
               rel:"delete-follower", method:"DELETE"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllOrganizationFollowers",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
