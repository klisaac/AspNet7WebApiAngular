using AspNet7.Application.Commands;
using FluentValidation;

namespace AspNet7.Application.Validations
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Method).NotEmpty();
        }
    }
}
