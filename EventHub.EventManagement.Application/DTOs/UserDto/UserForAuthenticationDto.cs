using System.ComponentModel.DataAnnotations;

namespace EventHub.EventManagement.Application.DTOs.UserDto
{
   public record UserForAuthenticationDto
   {
      [Required(ErrorMessage = "Email is required")]
      public string? Email { get; init; }

      [Required(ErrorMessage = "Password is required")]
      public string? Password { get; init; }



   }
}
