namespace EventHub.EventManagement.Domain.Entities.EventEntities
{
   public class Category
   {
      public Guid CategoryId { get; set; }
      public string? Name { get; set; }
      public Guid MediumId { get; set; }
      public Medium? Medium { get; set; }
      public ICollection<Event> Events { get; set; }

      public Category()
      {
         Events = new List<Event>();
      }

   }
}