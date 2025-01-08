namespace DotNetCore.Objects;

public static class GridExtensions
{
    public static Grid<T> Grid<T>(this IQueryable<T> queryable, GridParameters parameters)
    {
        var grid = new Grid<T>(queryable, parameters);

        return grid.List.Any() ? grid : default;
    }

    public static Task<Grid<T>> GridAsync<T>(this IQueryable<T> queryable, GridParameters parameters) => Task.FromResult(Grid(queryable, parameters));
}
