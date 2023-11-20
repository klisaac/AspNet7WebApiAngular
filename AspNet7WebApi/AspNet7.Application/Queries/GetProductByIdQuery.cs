using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
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
