using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Description).NotEmpty().NotNull().MinimumLength(2);

            RuleFor(c => c.DailyPrice).NotEmpty().NotNull().GreaterThanOrEqualTo(20).LessThanOrEqualTo(7600);

            RuleFor(c => c.BrandId).NotEmpty().NotNull();

            RuleFor(c => c.ColorId).NotEmpty().NotNull();

            RuleFor(c => c.ModelYear).NotEmpty().NotNull();
        }
    }
}
