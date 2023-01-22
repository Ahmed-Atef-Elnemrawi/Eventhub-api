using Microsoft.AspNetCore.Identity;

namespace EventHub.EventManagement.Domain.Entities.UserEntities
{
   public class User : IdentityUser
   {
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public int Age { get; set; }
      public Genre Genre { get; set; }
      public string? LiveIn { get; set; }
      public byte[]? ProfilePicture { get; set; }
      public UserPage? UserPage { get; set; }
      public string? RefreshToken { get; set; }
      public DateTime? RefreshTokenExpiryTime { get; set; }
      public string? FullName => $"{FirstName}, {LastName}";

      public override string ToString()
      {
         return $"Id:{Id}, Name:{FullName}, City:{LiveIn}, Email{Email}, Phone{PhoneNumber}";
      }
   }

   public enum Genre
   {
      none,
      male,
      female
   }
}
