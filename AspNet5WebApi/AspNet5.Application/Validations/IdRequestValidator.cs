using FluentValidation;

namespace AspNet5.Application.Validations
{
    public class IdRequestValidator : AbstractValidator<int>
    {
        public IdRequestValidator()
        {
            int id = 0;
            RuleFor(x => int.TryParse(x.ToString(), out id)).Must(y => y && id > 0).WithMessage("id cannout be null or 0.");
        }
    }
}
