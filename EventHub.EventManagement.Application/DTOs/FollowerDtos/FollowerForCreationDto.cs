using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.FollowerDtos
{
   public record FollowerForCreationDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public Genre Genre { get; init; }
      public int Age { get; set; }
      public string? LiveIn { get; init; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }

   }
}
