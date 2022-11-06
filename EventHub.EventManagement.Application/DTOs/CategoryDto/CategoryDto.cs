namespace EventHub.EventManagement.Application.DTOs.CategoryDto
{
   public record CategoryDto
   {
      public Guid CategoryId { get; init; }
      public string? Name { get; init; }
   }
}
