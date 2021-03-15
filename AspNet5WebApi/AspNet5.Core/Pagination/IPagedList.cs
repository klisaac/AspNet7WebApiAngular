using System.Collections.Generic;

namespace AspNet5.Core.Pagination
{
    public interface IPagedList<T>
    {
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        int TotalPages { get; set; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPreviousPage { get; set; }

        /// <summary>
        /// Has next age
        /// </summary>
        bool HasNextPage { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        IEnumerable<T> Items { get; set; }
    }
}
