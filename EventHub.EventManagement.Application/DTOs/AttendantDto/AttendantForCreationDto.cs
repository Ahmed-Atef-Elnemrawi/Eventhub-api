using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Application.DTOs.AttendantDto
{
    public record AttendantForCreationDto
   {
      public string? FirstName { get; init; }
      public string? LastName { get; set; }
      public Genre Gender { get; set; }
      public int Age { get; set; }
      public string? Phone { get; set; }
      public string? Email { get; set; }
      public string? LiveIn { get; set; }
      public string? FullName { get; set; }

   }
}
