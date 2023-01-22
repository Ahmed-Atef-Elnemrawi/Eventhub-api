using EventHub.EventManagement.Application.DTOs.OrganizationDtos;
using FluentValidation;

namespace EventHub.EventManagement.Application.Validation
{
   public class OrganizationValidator : AbstractValidator<OrganizationForManipulationDto>
   {


      public OrganizationValidator()
      {
         RuleFor(o => o.Name)
            .NotEmpty().WithMessage("organization name is required")
            .Length(3, 20).WithMessage("organization name length must be at least 3 characters");

         RuleFor(o => o.Country)
          .NotEmpty().WithMessage("Country is required");

         RuleFor(o => o.City)
            .NotEmpty().WithMessage("city is required");

         RuleFor(o => o.BusinessType)
            .NotEmpty().WithMessage("business type is required");

         RuleFor(o => o.BusinessDescription)
            .NotEmpty().WithMessage("business descrption is required");

         RuleFor(o => o.Email)
            .NotEmpty().WithMessage("organization email is required")
            .EmailAddress();

         RuleFor(o => o.PhoneNumber)
            .NotEmpty().WithMessage("phone number is required")
               .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");

      }

   }
}
