using DotNetCore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Objects
{
    public class Grid<T>
    {
        public Grid(IQueryable<T> queryable, GridParameters parameters)
        {
            Parameters = parameters;

            if (queryable == null || parameters == null) { return; }

            queryable = Filter(queryable, parameters.Filters);

            Count = queryable.LongCount();

            queryable = Order(queryable, parameters.Order);

            queryable = Page(queryable, parameters.Page);

            List = queryable.AsEnumerable();
        }

        public long Count { get; }

        public IEnumerable<T> List { get; }

        public GridParameters Parameters { get; }

        private static IQueryable<T> Filter(IQueryable<T> queryable, Filters filters)
        {
            if (filters == null) { return queryable; }

            foreach (var filter in filters)
            {
                queryable = queryable.Filter(filter.Property, filter.Comparison, filter.Value);
            }

            return queryable;
        }

        private static IQueryable<T> Order(IQueryable<T> queryable, Order order)
        {
            if (order == null) { return queryable; }

            return queryable.Order(order.Property, order.Ascending);
        }

        private static IQueryable<T> Page(IQueryable<T> queryable, Page page)
        {
            if (page == null) { return queryable; }

            return queryable.Page(page.Index, page.Size);
        }
    }
}
