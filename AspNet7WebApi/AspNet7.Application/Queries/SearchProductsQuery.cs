using MediatR;
using AspNet7.Application.Responses;
using AspNet7.Core.Pagination;

namespace AspNet7.Application.Queries
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
