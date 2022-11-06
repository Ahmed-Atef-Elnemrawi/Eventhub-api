using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.ProducerUtitilty
{
   public class ProducerLinks : EntityLinkes<ProducerDto>
   {

      public ProducerLinks(LinkGenerator linkGenerator)
         : base(linkGenerator)
      {

      }


      internal override List<Link> GenerateEntityLinks(HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields = "")
      {
         var producerId = id;
         var linkes = new List<Link>()
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetProducer", values: new {id, fields}),
            rel: "self",
            method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateProducer", values: new {id}),
            rel: "delete-producer",
            method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveProducer", values: new {id}),
            rel: "update-producer",
            method:"DELETE"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetProducerEvents",
            new {producerId}),
            rel:"get-producer-events", method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext, "CreateProducerEvent",
            new {producerId}),
            rel:"create-producer-event", method:"POST"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetProducerFollowers",
            new {producerId}),
            rel:"get-producer-followers", method:"GET"),

            new Link(_linkGenerator.GetUriByName(httpContext, "CreateProducerFollower",
            new {producerId}),
            rel:"create-producer-follower", method:"POST"),
         };

         return linkes;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllProducers", values: new { }),
           rel: "self",
           method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
