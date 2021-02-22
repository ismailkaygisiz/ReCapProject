using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.UserId).NotNull();

            RuleFor(c => c.Companyname).NotEmpty();
            RuleFor(c => c.Companyname).MinimumLength(2);
        }
    }
}
