namespace EventHub.EventManagement.Application.DTOs.EventDtos
{
   public record OrganizationEventDto : EventDto
   {
      public Guid OrganizationId { get; set; }
   }
}
