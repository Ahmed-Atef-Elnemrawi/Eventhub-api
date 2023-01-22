using EventHub.EventManagement.Application.DTOs.EventDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.EventUtility
{
   public class EventLinks : EntityLinkes<EventDto>
   {
      public EventLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields)
      {

         var mediumId = parentParentId;
         var categoryId = parentId;


         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetCategoryEvent",
            values: new {mediumId, categoryId, id}),
            rel:"self", method:"GET")
         };

         return links;
      }

      internal override LinkCollectionWrapper<Entity>
         GenerateEntitiesLinks(HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {

         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllCategoryEvents",
          values: new { }),
          rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
