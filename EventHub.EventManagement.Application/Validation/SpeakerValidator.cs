using EventHub.EventManagement.Application.DTOs.SpeakerDtos;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class SpeakerValidator : AbstractValidator<SpeakerForManipulationDto>
   {
      public SpeakerValidator()
      {
         RuleFor(s => s.FirstName)
            .NotEmpty().WithMessage("first name is required")
            .Length(3, 15)
            .WithMessage("first name must be at least 3 characters");

         RuleFor(s => s.LastName)
            .NotEmpty()
            .WithMessage("last name is required")
            .Length(3, 15)
            .WithMessage("last name must be at least 3 characters");

         RuleFor(s => s.Email)
            .EmailAddress()
             .NotEmpty()
             .WithMessage("email is required");

         RuleFor(s => s.JobTitle)
            .NotEmpty()
            .WithMessage("job title is required");

         RuleFor(s => s.Bio)
            .NotEmpty()
            .WithMessage("Bio is required")
            .Length(50, 10000).WithMessage("bio length must be at least 50 characters");

         RuleFor(s => s.Genre)
            .IsInEnum()
            .NotEmpty()
            .WithMessage("Genre is required");

         RuleFor(s => s.PhoneNumber)
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");


      }
   }
}
