using System;
using AspNet5.Core.Pagination.Base;

namespace AspNet5.Core.Pagination
{
    public interface IPagination: IBasePagination
    {
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
