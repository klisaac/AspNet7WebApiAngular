using System;
//using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AspNet5.Core.Specifications.Base
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        IList<Expression<Func<T, object>>> Includes { get; }
        //List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
        IList<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        bool IsDeleted { get; }
        int Take { get; }
        int Skip { get; }
        bool isPagingEnabled { get; }
    }
}
