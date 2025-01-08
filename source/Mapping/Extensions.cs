using AgileObjects.AgileMapper;

namespace DotNetCore.Mapping;

public static class Extensions
{
    public static TSource Clone<TSource>(this TSource source) => source is null ? default : Mapper.DeepClone(source);

    public static TDestination Map<TSource, TDestination>(this TSource source) => source is null ? default : Mapper.Map(source).ToANew<TDestination>();

    public static TDestination Map<TDestination>(this object source) => source is null ? default : Mapper.Map(source).ToANew<TDestination>();

    public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination) => Mapper.Map(source).OnTo(destination);

    public static IQueryable<TDestination> Project<TSource, TDestination>(this IQueryable<TSource> queryable) => queryable.Project().To<TDestination>();
}
