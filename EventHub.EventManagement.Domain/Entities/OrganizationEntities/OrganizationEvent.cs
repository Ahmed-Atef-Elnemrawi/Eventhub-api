using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Domain.Entities.OrganizationEntities
{
   public class OrganizationEvent : Event
   {
      public Guid OrganizationId { get; set; }
      public Organization? Organization { get; set; }
      public ICollection<Speaker> Speakers { get; set; }

      public OrganizationEvent()
      {
         Speakers = new List<Speaker>();
      }

   }


}
