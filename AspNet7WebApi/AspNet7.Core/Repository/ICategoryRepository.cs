using System.Threading.Tasks;
using AspNet7.Core.Entities;
using AspNet7.Core.Pagination;
using AspNet7.Core.Repository.Base;

namespace AspNet7.Core.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IPagedList<Category>> SearchCategoriesAsync(SearchArgs args);
    }
}
