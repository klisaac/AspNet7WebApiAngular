using AspNet5.Application.Commands;
using FluentValidation;

namespace AspNet5.Application.Validations
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(request => request.UserName).NotEmpty();
            RuleFor(request => request.Password).NotEmpty();
        }
    }
}
