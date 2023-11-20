using AspNet7.Application.Commands;
using FluentValidation;

namespace AspNet7.Application.Validations
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
