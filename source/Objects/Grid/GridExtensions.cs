using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Objects
{
    public static class GridExtensions
    {
        public static Grid<T> Grid<T>(this IQueryable<T> queryable, GridParameters parameters)
        {
            return new(queryable, parameters);
        }

        public static Task<Grid<T>> GridAsync<T>(this IQueryable<T> queryable, GridParameters parameters)
        {
            return Task.FromResult(new Grid<T>(queryable, parameters));
        }
    }
}
