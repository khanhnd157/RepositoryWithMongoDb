
using MongoDB.Driver;

namespace MazeCore.MongoDb.Context
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
