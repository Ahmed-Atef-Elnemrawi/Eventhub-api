namespace EventHub.EventManagement.Application.DTOs.EventDtos
{
   public record ProducerEventDto : EventDto
   {
      public Guid ProducerId { get; set; }
   }
}
