using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Objects
{
    public static class Extensions
    {
        public static Grid<T> List<T>(this IQueryable<T> queryable, GridParameters parameters)
        {
            return new Grid<T>(queryable, parameters);
        }

        public static Task<Grid<T>> ListAsync<T>(this IQueryable<T> queryable, GridParameters parameters)
        {
            return Task.FromResult(new Grid<T>(queryable, parameters));
        }
    }
}
