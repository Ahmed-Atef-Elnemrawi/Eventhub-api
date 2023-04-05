using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.EventManagement.Domain.Entities
{
   public class User : IdentityUser
   {
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public int Age { get; set; }
      public Genre Genre { get; set; }
      public string? LiveIn { get; set; }
      public byte[]? ProfilePicture { get; set; }
      public string? FullName => $"{FirstName}, {LastName}";
      public string? RefreshToken { get; set; }
      public DateTime? RefreshTokenExpiryTime { get; set; }
      [ForeignKey("UserPage")]
      public Guid? UserPageId { get; set; }
      public UserPage? UserPage { get; set; }

   }

   public enum Genre
   {
      none,
      male,
      female
   }
}
