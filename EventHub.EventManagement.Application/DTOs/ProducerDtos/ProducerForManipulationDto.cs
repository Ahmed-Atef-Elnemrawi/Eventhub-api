using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.ProducerDtos
{
   public record ProducerForManipulationDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; init; }
      public int Age { get; set; }
      public Genre Genre { get; init; }
      public string? LiveIn { get; init; }
      public string? Email { get; init; }
      public string? PhoneNumber { get; init; }
      public string? JobTitle { get; init; }
      public string? Bio { get; init; }
   }
}
