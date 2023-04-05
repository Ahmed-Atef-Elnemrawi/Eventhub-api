using EventHub.EventManagement.Domain.Entities;

namespace EventHub.EventManagement.Application.DTOs.FollowerDto
{
   public record FollowerForCreationDto
   {
      public string? FollowerId { get; init; }
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public Genre Genre { get; init; }
      public int Age { get; set; }
      public string? LiveIn { get; init; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }

   }
}
