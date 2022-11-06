namespace EventHub.EventManagement.Application.DTOs.EventDto
{
   public record OrganizationEventForCreationDto : EventForManipulationDto
   {
      public List<Guid> SpeakersId { get; set; }

      public OrganizationEventForCreationDto()
      {
         SpeakersId = new List<Guid>();
      }
   }
}
