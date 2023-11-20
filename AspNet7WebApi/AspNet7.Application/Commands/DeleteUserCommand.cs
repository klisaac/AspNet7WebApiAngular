using MediatR;

namespace AspNet7.Application.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
