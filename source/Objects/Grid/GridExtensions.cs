namespace DotNetCore.Objects;

public static class GridExtensions
{
    extension<T>(IQueryable<T> queryable)
    {
        public Grid<T> Grid(GridParameters parameters)
        {
            var grid = new Grid<T>(queryable, parameters);

            return grid.List.Any() ? grid : null;
        }

        public Task<Grid<T>> GridAsync(GridParameters parameters) => Task.FromResult(Grid(queryable, parameters));
    }
}
