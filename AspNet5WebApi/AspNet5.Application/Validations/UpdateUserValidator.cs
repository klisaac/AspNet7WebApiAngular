using AspNet5.Application.Commands;
using FluentValidation;

namespace AspNet5.Application.Validations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password & Confirm password do not match.");
            //RuleFor(x => x.Password == x.ConfirmPassword);
        }
    }
}
