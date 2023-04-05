

namespace EventHub.EventManagement.Application.DTOs.EventDto
{
   public record ProducerEventDto : EventDto
   {
      public ProducerDto.ProducerDto? Producer { get; init; }
      public CategoryDto.CategoryDto? Category { get; init; }

   }
}
