using EventHub.EventManagement.Application.DTOs.UserDto;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class UserValidator : AbstractValidator<UserForRegistrationDto>
   {
      public UserValidator()
      {
         RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("first name is required")
            .Length(3, 15).WithMessage("first name must be at least 3 characters");

         RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("last name is required")
            .Length(3, 15).WithMessage("last name must be at least 3 characters");

         RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("last name is required")
            .Length(5, 15).WithMessage("last name must be at least 5 characters");

         RuleFor(u => u.Password)
            .NotEmpty().WithMessage("password is required")
            .MinimumLength(10).WithMessage("password should be at least 10 characters");

         RuleFor(u => u.Genre)
            .NotEmpty().WithMessage("genre is required")
            .IsInEnum();

         RuleFor(u => u.LiveIn)
            .NotEmpty().WithMessage("city is required");

         RuleFor(u => u.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress();

         RuleFor(u => u.PhoneNumber)
            .NotEmpty().WithMessage("phone number is required");

         RuleFor(u => u.Age)
            .NotEmpty().WithMessage("age is required")
            .GreaterThan(10).WithMessage("age should be greater than 10 years")
            .LessThan(100).WithMessage("age should be less thant 80 years");

      }

   }
}
