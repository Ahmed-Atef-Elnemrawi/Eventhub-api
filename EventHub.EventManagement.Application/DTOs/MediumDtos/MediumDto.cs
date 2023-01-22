namespace EventHub.EventManagement.Application.DTOs.MediumDtos
{
   public record MediumDto
   {
      public Guid MediumId { get; init; }
      public string? Type { get; init; }
   }
}
