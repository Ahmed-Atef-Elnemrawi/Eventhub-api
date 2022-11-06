using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Domain.Entities.ProducerEntities
{
   public class ProducerEvent : Event
   {
      public Guid ProducerId { get; set; }
      public Producer? Producer { get; set; }
   }


}
