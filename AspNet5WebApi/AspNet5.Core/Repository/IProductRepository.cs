using AspNet5.Core.Entities;
using AspNet5.Core.Pagination;
using AspNet5.Core.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNet5.Core.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IPagedList<Product>> SearchProductsAsync(SearchArgs args);
    }
}
