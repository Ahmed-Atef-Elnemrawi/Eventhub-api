using System.ComponentModel.DataAnnotations;

namespace EventHub.EventManagement.Application.DTOs.SpeakerDtos
{
   public record EventSpeakerForCreationDto
   {
      [Required(ErrorMessage = "organization speaker is required")]
      public Guid SpeakerId { get; set; }
   }
}
