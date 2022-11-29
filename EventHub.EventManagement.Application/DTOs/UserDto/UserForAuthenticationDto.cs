using System.ComponentModel.DataAnnotations;

namespace EventHub.EventManagement.Application.DTOs.UserDto
{
   public record UserForAuthenticationDto
   {
      [Required(ErrorMessage = "user name is required")]
      public string? UserName { get; init; }

      [Required(ErrorMessage = "password is required")]
      public string? Password { get; init; }



   }
}
