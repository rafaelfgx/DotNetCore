using MongoDB.Driver;

namespace DotNetCore.MongoDB
{
    public static class Filters
    {
        public static FilterDefinition<T> Id<T>(object value)
        {
            return Builders<T>.Filter.Eq(nameof(Id), value);
        }
    }
}
