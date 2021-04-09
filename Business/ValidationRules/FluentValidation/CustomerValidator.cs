using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().NotNull();

            RuleFor(c => c.CompanyName).NotEmpty().NotNull().MinimumLength(2);
        }
    }
}
