using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.ProducerUtitilty
{
   public class ProducerFollowerLinks : EntityLinkes<FollowerDto>
   {
      public ProducerFollowerLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks(
        HttpContext httpContext,
        Guid id,
        Guid? parentId,
        Guid? parentParentId,
        string fields = "")
      {
         var producerId = parentId;

         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetProducerFollower",
               values: new {producerId , id, fields}),
               rel:"self", method:"GET"),


            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveProducerFollower",
               values: new {producerId , id}),
               rel:"delete-follower", method:"DELETE"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllProducerFollowerss",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
