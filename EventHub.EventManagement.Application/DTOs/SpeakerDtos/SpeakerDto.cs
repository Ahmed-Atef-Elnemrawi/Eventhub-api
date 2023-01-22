namespace EventHub.EventManagement.Application.DTOs.SpeakerDtos
{
   public record SpeakerDto
   {
      public Guid SpeakerId { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? FullName { get; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public string? Genre { get; init; }
      public string? JobTitle { get; init; }
      public string? Bio { get; init; }
   }
}
