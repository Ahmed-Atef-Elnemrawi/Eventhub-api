namespace EventHub.EventManagement.Application.DTOs.EventDtos
{
   public record EventForManipulationDto
   {
      public Guid CategoryId { get; set; }
      public string? Name { get; init; }
      public DateTime Date { get; init; }
      public string? Description { get; init; }
      public byte[]? Image { get; init; }
      public string? Url { get; init; }
      public string? City { get; init; }
      public string? Country { get; init; }
   }
}
