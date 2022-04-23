using MongoDB.Driver;

namespace SNA_Directory.Data.DataAccess;

public interface IDbConnection
{
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<SNAModel> SNACollection { get; }
    string SNACollectionName { get; }
    IMongoCollection<Biome> BiomeCollection { get; }
    string BiomeCollectionName { get; }
    IMongoCollection<CommentModel> CommentCollection { get; }
    string CommentCollectionName { get; }
}