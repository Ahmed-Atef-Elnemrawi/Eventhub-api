using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.UserDto
{
    public record UserProfileDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? UserName { get; init; }
      public int Age { get; init; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public Genre Genre { get; init; }
      public string? Country { get; init; }
      public byte[]? ProfilePicture { get; init; }

   }
}
