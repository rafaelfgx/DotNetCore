using MongoDB.Bson;

namespace DotNetCore.MongoDB
{
    public interface IDocument
    {
        ObjectId Id { get; set; }
    }
}
