using EventHub.EventManagement.Application.DTOs.SpeakerDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.OrganizationUtility
{
   public class SpeakerLinks : EntityLinkes<SpeakerDto>
   {
      public SpeakerLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }
      internal override List<Link> GenerateEntityLinks(HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields = "")
      {
         var organizationId = parentId;
         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetOrganizationSpeaker",
               values: new {organizationId, id, fields}),
               rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateOrganizationSpeaker",
               values: new { organizationId,id}),
               rel:"update-organization", method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveOrganizationSpeaker",
               values: new { organizationId,id}),
               rel:"delete-organization", method:"DELETE"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllOrganizationSpeakers",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
