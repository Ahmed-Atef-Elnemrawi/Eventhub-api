using System.ComponentModel.DataAnnotations;

namespace EventHub.EventManagement.Application.DTOs
{
   public record ForgotPasswordDto
   {
      [Required]
      [EmailAddress]
      public string? Email { get; init; }

      [Required]
      public string? ResetPasswordClientURI { get; set; }
   }
}
