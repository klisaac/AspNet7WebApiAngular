using MediatR;
using AspNet7.Application.Responses;
using AspNet7.Core.Pagination;

namespace AspNet7.Application.Queries
{
    public class SearchCategoriesQuery: IRequest<IPagedList<CategoryResponse>>
    {
        public SearchArgs Args { get; set; }
        public SearchCategoriesQuery(SearchArgs args)
        {
            Args = args;
        }
    }
}
