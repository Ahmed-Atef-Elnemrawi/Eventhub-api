using EventHub.EventManagement.Domain.Common;

namespace EventHub.EventManagement.Domain.Entities.EventEntities;

public class Attendant : ISortableEntity, ISearchableEntity
{
   public Guid AttendantId { get; set; }
   public string? FirstName { get; set; }
   public string? LastName { get; set; }
   public Genre? Genre { get; set; }
   public int Age { get; set; }
   public string? City { get; set; }
   public string? PhoneNumber { get; set; }
   public string? Email { get; set; }
   public ICollection<Event>? Events { get; set; }

}