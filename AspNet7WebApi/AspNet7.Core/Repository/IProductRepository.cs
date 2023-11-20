using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet7.Core.Entities;
using AspNet7.Core.Pagination;
using AspNet7.Core.Repository.Base;

namespace AspNet7.Core.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IPagedList<Product>> SearchProductsAsync(SearchArgs args);
    }
}
