using MediatR;
using AspNet5.Core.Pagination;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
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
