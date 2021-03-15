using MediatR;

namespace AspNet5.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}
