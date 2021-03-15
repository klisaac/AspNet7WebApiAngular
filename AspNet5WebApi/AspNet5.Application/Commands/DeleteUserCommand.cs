using MediatR;

namespace AspNet5.Application.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
