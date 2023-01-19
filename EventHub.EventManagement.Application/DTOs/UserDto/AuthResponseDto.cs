namespace EventHub.EventManagement.Application.DTOs.UserDto
{
   public record AuthResponseDto
   {
      public TokenDto? TokenDto { get; init; }
      public UserProfileDto? UserProfile { get; init; }
   }
}
