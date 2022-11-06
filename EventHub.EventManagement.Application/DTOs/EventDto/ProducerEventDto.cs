namespace EventHub.EventManagement.Application.DTOs.EventDto
{
   public record ProducerEventDto : EventDto
   {
      public Guid ProducerId { get; set; }
   }
}
