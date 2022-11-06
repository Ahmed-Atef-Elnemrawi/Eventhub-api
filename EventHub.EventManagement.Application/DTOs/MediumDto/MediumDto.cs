namespace EventHub.EventManagement.Application.DTOs.MediumDto
{
   public record MediumDto
   {
      public Guid MediumId { get; init; }
      public string? Type { get; init; }
   }
}
