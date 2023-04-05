using EventHub.EventManagement.Domain.Common;

namespace EventHub.EventManagement.Domain.Entities.EventEntities
{
   public class Event : AuditableEntity, ISortableEntity, ISearchableEntity
   {
      public Guid EventId { get; set; }
      public string? Name { get; set; }
      public DateTime Date { get; set; }
      public string? Description { get; set; }
      public byte[]? Image { get; set; }
      public string? Url { get; set; }
      public string? City { get; set; }
      public string? Country { get; set; }
      public string? Discriminator { get; set; }
      public Guid CategoryId { get; set; }
      public EventState EventState { get; set; }
      public Category? Category { get; set; }
      public ICollection<Attendant> Attendants { get; set; }

      public Event()
      {
         Attendants = new List<Attendant>();
      }

      public bool HasValidDate(DateTime date)
      {
         int value = DateTime.UtcNow.CompareTo(date);
         if (value < 0) return true;
         if (value == 0) return true;
         return false;

      }
   }

   public enum EventState
   {
      Upcoming,
      Ongoing,
      Finished
   }
}
