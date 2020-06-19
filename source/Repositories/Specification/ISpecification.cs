using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotNetCore.Repositories
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

        int Skip { get; }

        int Take { get; }

        Expression<Func<T, bool>> Where { get; }

        void AddInclude(Expression<Func<T, object>> include);

        void ApplyOrderBy(Expression<Func<T, object>> orderBy);

        void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending);

        void ApplySkipTake(int skip, int take);

        void ApplyWhere(Expression<Func<T, bool>> where);
    }
}
