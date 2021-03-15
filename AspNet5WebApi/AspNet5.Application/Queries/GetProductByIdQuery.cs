using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetProductByIdQuery: IRequest<ProductResponse>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
