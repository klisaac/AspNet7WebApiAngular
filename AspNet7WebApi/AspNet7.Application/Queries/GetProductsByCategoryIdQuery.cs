using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
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
