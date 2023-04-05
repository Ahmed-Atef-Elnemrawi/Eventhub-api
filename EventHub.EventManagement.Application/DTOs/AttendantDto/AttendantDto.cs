using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.DTOs.AttendantDto
{
   public record AttendantDto
   {
      public Guid AttendantId { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public Genre? Genre { get; init; }
      public int Age { get; init; }
      public string? City { get; init; }
      public string? PhoneNumber { get; init; }
      public string? Email { get; init; }
   }
}
