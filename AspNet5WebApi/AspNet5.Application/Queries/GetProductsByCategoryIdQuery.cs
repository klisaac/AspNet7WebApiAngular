using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetProductsByCategoryIdQuery: IRequest<ProductResponse>
    {
        public int CategoryId { get; set; }

        public GetProductsByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
