namespace EventHub.EventManagement.Application.DTOs.CategoryDtos
{
   public record CategoryDto
   {
      public Guid CategoryId { get; init; }
      public string? Name { get; init; }
   }
}
