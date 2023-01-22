using System.ComponentModel.DataAnnotations;

namespace EventHub.EventManagement.Application.DTOs.UserDtos
{
   public record UserForAuthenticationDto
   {
      [Required(ErrorMessage = "Email is required")]
      [DataType(DataType.EmailAddress)]
      public string? Email { get; init; }

      [Required(ErrorMessage = "Password is required")]
      [DataType(DataType.Password)]
      public string? Password { get; init; }



   }
}
