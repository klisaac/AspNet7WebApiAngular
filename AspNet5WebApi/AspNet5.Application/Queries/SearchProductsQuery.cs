using MediatR;
using AspNet5.Core.Pagination;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class SearchProductsQuery: IRequest<IPagedList<ProductResponse>>
    {
        public SearchArgs Args { get; set; }
        public SearchProductsQuery(SearchArgs args)
        {
            Args = args;
        }
    }
}
