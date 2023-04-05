using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.ProducerUtitilty
{
   public class ProducerEventLinkes : EntityLinkes<ProducerEventDto>
   {
      public ProducerEventLinkes(LinkGenerator linkGenerator) : base(linkGenerator)
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
         var producerId = parentId;

         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetProducerEvent",
               values: new {producerId , id, fields}),
               rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateProducerEvent",
               values: new {producerId , id}),
               rel:"update-event", method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveProducerEvent",
               values: new {producerId , id}),
               rel:"delete-event", method:"DELETE"),

            new Link(_linkGenerator.GetUriByName(httpContext, "GetProducerEventAttendants",
               new {producerId , eventId}),
               rel:"get-producer-event-attendants", method:"GET"),

             new Link(_linkGenerator.GetUriByName(httpContext, "CreateProducerEventAttendant",
               new {producerId , eventId}),
               rel:"create-producer-event-attendant", method:"POST"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks(
         HttpContext httpContext,
         LinkCollectionWrapper<Entity> entitiesCollection)
      {


         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllProducerEvents",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
