using EventHub.EventManagement.Application.DTOs.EventDto;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.EventUtility
{
   public class CategoryProducerEventLinks : EntityLinkes<EventDto>
   {
      public CategoryProducerEventLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {

      }

      internal override List<Link> GenerateEntityLinks
         (HttpContext httpContext, Guid id, Guid? parentId, Guid? parentParentId, string fields)
      {
         var producerId = parentId;
         var eventId = id;
         var links = new List<Link>
        {
           new Link(_linkGenerator.GetUriByName(httpContext,"GetProducer",
           new{id = producerId}),
           rel:"producer", method:"GET"),

           new Link(_linkGenerator.GetUriByName(httpContext,"CreateProducerFollower",
           new{producerId}),
           rel:"create-producer-follower", method:"POST"),

           new Link(_linkGenerator.GetUriByName(httpContext,"CreateProducerEventAttendant",
           new{producerId, eventId}),
           rel:"create-event-attendant", method:"POST"),


        };

         return links;
      }
   }
}

