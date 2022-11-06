using EventHub.EventManagement.Domain.Common;

namespace EventHub.EventManagement.Domain.Entities.OrganizationEntities
{
   public class Organization : ISortableEntity, ISearchableEntity
   {
      public Guid OrganizationId { get; set; }
      public string? Name { get; set; }
      public string? BusinessType { get; set; }
      public string? BusinessDescription { get; set; }
      public string? Country { get; set; }
      public string? City { get; set; }
      public string? Email { get; set; }
      public ICollection<OrganizationEvent> OrganizationEvents { get; set; }
      public ICollection<Speaker> Speakers { get; set; }
      public ICollection<Follower> Followers { get; set; }

      public Organization()
      {
         OrganizationEvents = new List<OrganizationEvent>();
         Speakers = new List<Speaker>();
         Followers = new List<Follower>();
      }
   }



}
