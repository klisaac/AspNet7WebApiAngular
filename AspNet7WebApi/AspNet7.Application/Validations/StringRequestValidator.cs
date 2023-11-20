using FluentValidation;

namespace AspNet7.Application.Validations
{
    public class StringRequestValidator : AbstractValidator<string>
    {
        public StringRequestValidator()
        {
            RuleFor(x => x.ToString()).NotEmpty().WithMessage("Get parameter value cannout be null or empty.");
        }
    }
}
