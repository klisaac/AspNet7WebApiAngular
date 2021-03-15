using MediatR;

namespace AspNet5.Application.Commands
{
    public class LoginUserCommand: IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
