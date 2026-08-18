using MongoDB.Driver;

namespace DotNetCore.MongoDB;

public static class Filters
{
    public static FilterDefinition<T> Id<T>(object value) => Builders<T>.Filter.Eq(nameof(Id), value);
}
