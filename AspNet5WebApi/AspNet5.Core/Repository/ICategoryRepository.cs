using AspNet5.Core.Entities;
using AspNet5.Core.Pagination;
using AspNet5.Core.Repository.Base;
using System.Threading.Tasks;

namespace AspNet5.Core.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IPagedList<Category>> SearchCategoriesAsync(SearchArgs args);
    }
}
