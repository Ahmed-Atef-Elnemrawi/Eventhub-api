namespace EventHub.EventManagement.Application.DTOs.EventDto
{
   public record OrganizationEventDto : EventDto
   {
      public Guid OrganizationId { get; set; }
   }
}
