using EventHub.EventManagement.Application.DTOs.AttendantDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.ProducerUtitilty
{
   public class ProducerEventAttendantLinks : EntityLinkes<AttendantDto>
   {
      public ProducerEventAttendantLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks(
        HttpContext httpContext,
        Guid id,
        Guid? parentId,
        Guid? parentParentId,
        string fields = "")
      {

         var eventId = parentId;
         var producerId = parentParentId;

         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetProducerEventAttendant",
               values: new {producerId, eventId, id, fields}),
               rel:"self", method:"GET"),


            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveProducerEventAttendant",
               values: new {producerId, eventId, id}),
               rel:"delete-attendant", method:"DELETE"),

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks(
         HttpContext httpContext,
         LinkCollectionWrapper<Entity> entitiesCollection)
      {

         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllProducerEventAttendants",
            values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }
   }
}
