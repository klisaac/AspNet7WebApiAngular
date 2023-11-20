using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNet7.Core.Entities;
using AspNet7.Core.Pagination;
using AspNet7.Core.Repository;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Pagination;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AspNet7DataContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IPagedList<Category>> SearchCategoriesAsync(SearchArgs args)
        {
            var query = Table;
            var orderByList = new List<Tuple<SortingOption, Expression<Func<Category, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Category, object>>>(sortingOption, c => c.CategoryId));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Category, object>>>(sortingOption, c => c.Name));
                            break;
                        case "description":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Category, object>>>(sortingOption, c => c.Description));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Category, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.CategoryId));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<Category, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Category, bool>>>(filteringOption, c => c.CategoryId == Convert.ToInt32(filteringOption.Value)));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Category, bool>>>(filteringOption, c => c.Name.Contains((string)filteringOption.Value)));
                            break;
                        case "description":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Category, bool>>>(filteringOption, c => c.Description.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var categoryPagedList = new PagedList<Category>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Category>>(categoryPagedList);
        }
    }
}
