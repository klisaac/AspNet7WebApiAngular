
namespace AspNet5.Core.Pagination.Base
{
    public interface IBasePagination
    {
        int PageIndex { get; }
        int PageSize { get; }
        string SortBy { get; }
    }
}
