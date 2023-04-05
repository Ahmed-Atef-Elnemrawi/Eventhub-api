using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.DTOs.UserDto
{
   public record class UserProfileForUpdateDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public string? UserName { get; init; }
      public int Age { get; init; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public Genre Genre { get; init; }
      public string? LiveIn { get; init; }
      public byte[]? ProfilePicture { get; init; }
      public UserPage? UserPage { get; init; }
      public Guid? UserPageId { get; set; }
   }
}
