using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Domain.Entities.OrganizationEntities
{
    public class Speaker
   {
      public Guid SpeakerId { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? FullName { get; }
      public Genre Genre { get; set; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }
      public string? JobTitle { get; set; }
      public string? Bio { get; set; }
      public Guid OrganizationId { get; set; }
      public Organization? Organization { get; set; }
      public ICollection<OrganizationEvent> OrganizationEvents { get; set; }

      public Speaker()
      {
         OrganizationEvents = new List<OrganizationEvent>();
      }


   }
}
