using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.SpeakerDto
{
    public record SpeakerForManipulationDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public Genre Genre { get; init; }
      public string? JobTitle { get; init; }
      public string? Bio { get; init; }
   }
}
