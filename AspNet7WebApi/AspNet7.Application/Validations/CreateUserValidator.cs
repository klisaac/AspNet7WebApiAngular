using AspNet7.Application.Commands;
using FluentValidation;

namespace AspNet7.Application.Validations
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password & Confirm password do not match.");
            RuleFor(x => x.Password == x.ConfirmPassword);
        }
    }
}
