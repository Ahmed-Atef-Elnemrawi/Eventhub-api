namespace EventHub.EventManagement.Application.DTOs.OrganizationDto
{
   public record OrganizationDto
   {
      public Guid OrganizationId { get; init; }
      public string? Name { get; init; }
      public string? BusinessType { get; init; }
      public string? BusinessDescription { get; init; }
      public string? Country { get; init; }
      public string? City { get; init; }
      public string? Email { get; init; }
   }
}
