using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CarId).NotNull();

            RuleFor(r => r.CustomerId).NotEmpty();
            RuleFor(r => r.CustomerId).NotNull();
        }
    }
}
