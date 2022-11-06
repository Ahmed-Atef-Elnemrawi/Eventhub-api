namespace EventHub.EventManagement.Application.DTOs.AttendantDto
{
   public record AttendantDto
   {
      public Guid AttendantId { get; set; }
      public string? FirstName { get; init; }
      public string? LastName { get; set; }
      public int Age { get; set; }
      public string? Genre { get; set; }
      public string? Phone { get; set; }
      public string? Email { get; set; }
      public string? LiveIn { get; set; }
      public string? FullName { get; set; }
      public string? Gender { get; set; }
   }
}
