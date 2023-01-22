using EventHub.EventManagement.Application.DTOs.ProducerDtos;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class ProducerValidator : AbstractValidator<ProducerForManipulationDto>
   {
      public ProducerValidator()
      {
         RuleFor(p => p.FirstName)
           .NotEmpty().WithMessage("first name is required")
           .Length(3, 15).WithMessage("first name must be at least 3 characters");

         RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("last name is required")
            .Length(3, 15).WithMessage("last name must be at least 3 characters");

         RuleFor(p => p.Genre)
            .NotEmpty().WithMessage("genre is required")
            .IsInEnum();

         RuleFor(p => p.LiveIn)
            .NotEmpty().WithMessage("city is required");

         RuleFor(p => p.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress();

         RuleFor(p => p.PhoneNumber)
            .NotEmpty().WithMessage("phone number is required")
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");

         RuleFor(p => p.Age)
            .NotEmpty().WithMessage("age is required")
            .GreaterThan(10).WithMessage("age should be greater than 10 years")
            .LessThan(100).WithMessage("age should be less thant 80 years");

         RuleFor(p => p.Bio)
         .NotEmpty()
         .WithMessage("Bio is required")
         .Length(50, 10000).WithMessage("bio length must be at least 50 characters");
      }
   }
}
