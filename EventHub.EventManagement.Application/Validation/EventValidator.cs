using EventHub.EventManagement.Application.DTOs.EventDto;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class EventValidator : AbstractValidator<EventForManipulationDto>
   {
      public EventValidator()
      {
         RuleFor(e => e.Name)
            .NotEmpty().WithMessage("event name is required")
            .MinimumLength(3).WithMessage("event name must be at least 10 characters");

         RuleFor(e => e.CategoryId)
            .NotEmpty().WithMessage("category id is requried");

         RuleFor(e => e.Date)
            .NotEmpty().WithMessage("event date is required")
            .Must(e => e >= DateTime.UtcNow).WithMessage("event date must be greater from now");

         RuleFor(e => e.Description)
            .NotEmpty().WithMessage("event description is required")
            .Length(10, 10000).WithName("description length");

         //RuleFor(e => e.Image)
         //   .NotNull().WithMessage("event image is required");
      }
   }
}
