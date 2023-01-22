namespace EventHub.EventManagement.Application.DTOs.UserDtos
{
   public record AuthResponseDto
   {
      public TokenDto? TokenDto { get; init; }
      public UserProfileDto? UserProfile { get; init; }
   }
}
