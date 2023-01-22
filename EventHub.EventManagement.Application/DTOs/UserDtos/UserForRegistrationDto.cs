using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.UserDtos;

public record UserForRegistrationDto
{
   public UserForRegistrationDto() { }
   public string? FirstName { get; init; }
   public string? LastName { get; init; }
   public string? UserName { get; init; }
   public string? Password { get; init; }
   public string? Email { get; init; }
   public string? PhoneNumber { get; init; }
   public int Age { get; init; }
   public Genre Genre { get; init; }
   public string? LiveIn { get; init; }
   public byte[]? ProfilePicture { get; init; }
   public ICollection<string>? Roles { get; init; }
}
