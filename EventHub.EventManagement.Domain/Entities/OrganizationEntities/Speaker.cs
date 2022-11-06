namespace EventHub.EventManagement.Domain.Entities.OrganizationEntities
{
   public class Speaker : IEquatable<Speaker?>
   {
      public Guid SpeakerId { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public string? FullName { get; }
      public Genre Genre { get; set; }
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

      public override bool Equals(object? obj)
      {
         return Equals(obj as Speaker);
      }

      public bool Equals(Speaker? other)
      {
         return other is not null &&
                FirstName == other.FirstName &&
                LastName == other.LastName &&
                Genre == other.Genre &&
                Email == other.Email &&
                JobTitle == other.JobTitle &&
                OrganizationId.Equals(other.OrganizationId);
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(FirstName, LastName, Genre, Email, JobTitle, OrganizationId);
      }

      public static bool operator ==(Speaker? left, Speaker? right)
      {
         return EqualityComparer<Speaker>.Default.Equals(left, right);
      }

      public static bool operator !=(Speaker? left, Speaker? right)
      {
         return !(left == right);
      }
   }
}
