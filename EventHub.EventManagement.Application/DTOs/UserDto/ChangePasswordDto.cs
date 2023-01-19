namespace EventHub.EventManagement.Application.DTOs.UserDto
{
   public record ChangePasswordDto
   {
      public string? CurrentPassword { get; init; }
      public string? NewPassword { get; set; }
   }
}
