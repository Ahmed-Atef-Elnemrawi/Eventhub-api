using EventHub.EventManagement.Application.DTOs.MediumDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.EventUtility
{
   public class MediumLinks : EntityLinkes<MediumDto>
   {
      public MediumLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }


      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields = "")
      {
         var mediumId = id;
         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetMedium", values: new {id}),
            rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext,"GetCategories",values:new{mediumId}),
            rel:"get-medium-categories", method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext,"CreateCategory", new{mediumId}),
            rel:"create-category", method:"Post")
         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllMediums", values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
