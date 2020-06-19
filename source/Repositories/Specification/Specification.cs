using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotNetCore.Repositories
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public Expression<Func<T, bool>> Where { get; private set; }

        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        public void ApplyOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        public void ApplySkipTake(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public void ApplyWhere(Expression<Func<T, bool>> where)
        {
            Where = where;
        }
    }
}
