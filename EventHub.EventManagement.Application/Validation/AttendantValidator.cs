using EventHub.EventManagement.Application.DTOs.AttendantDto;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class AttendantValidator : AbstractValidator<AttendantForCreationDto>
   {
      public AttendantValidator()
      {
         RuleFor(attendant => attendant.FirstName)
            .NotEmpty().WithMessage("first name is required")
            .Length(3, 15).WithMessage("name length should be a least 3 characters");

         RuleFor(attendant => attendant.LastName)
            .NotEmpty().WithMessage("last name is required")
            .Length(3, 15).WithMessage("name length should be a least 3 characters");

         RuleFor(attendant => attendant.Genre)
            .NotEmpty().WithMessage("genre is required")
            .IsInEnum();

         RuleFor(attendant => attendant.City)
            .NotEmpty().WithMessage("city is required");

         RuleFor(attendant => attendant.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress();

         RuleFor(attendat => attendat.PhoneNumber)
            .NotEmpty().WithMessage("phone number is required")
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");

         RuleFor(attendat => attendat.Age)
            .NotEmpty().WithMessage("age is required")
            .GreaterThan(10).WithMessage("age should be greater than 10 years")
            .LessThan(100).WithMessage("age should be less thant 80 years");
      }
   }
}
