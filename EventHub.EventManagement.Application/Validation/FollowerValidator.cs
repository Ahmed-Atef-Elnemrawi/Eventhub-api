using EventHub.EventManagement.Application.DTOs.FollowerDtos;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class FollowerValidator : AbstractValidator<FollowerForCreationDto>
   {
      public FollowerValidator()
      {
         RuleFor(f => f.FirstName)
            .NotEmpty().WithMessage("first name is required")
            .Length(3, 15).WithMessage("first name must be at least 3 characters");

         RuleFor(f => f.LastName)
            .NotEmpty().WithMessage("last name is required")
            .Length(3, 15).WithMessage("last name must be at least 3 characters");

         RuleFor(f => f.Genre)
            .NotEmpty().WithMessage("genre is required")
            .IsInEnum();

         RuleFor(f => f.LiveIn)
            .NotEmpty().WithMessage("city is required");

         RuleFor(f => f.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress();

         RuleFor(f => f.PhoneNumber)
            .NotEmpty().WithMessage("phone number is required")
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");

         RuleFor(f => f.Age)
            .NotEmpty().WithMessage("age is required")
            .GreaterThan(10).WithMessage("age should be greater than 10 years")
            .LessThan(100).WithMessage("age should be less thant 80 years");
      }
   }
}
