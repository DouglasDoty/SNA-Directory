using MongoDB.Driver;

namespace SNA_Directory.Data.DataAccess;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _database;
    private readonly string _connectionId = "MongoDB";
    public string DbName { get; private set; }
    public string SNACollectionName { get; private set; } = "snas";
    public string BiomeCollectionName { get; private set; } = "biomes";
    public string CommentCollectionName { get; private set; } = "comments";
    public MongoClient Client { get; private set; }
    public IMongoCollection<AreaModel> AreaCollection { get; private set; }
    public IMongoCollection<Biome> BiomeCollection { get; private set; }
    public IMongoCollection<CommentModel> CommentCollection { get; private set; }

    public DbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        Client = new MongoClient(_configuration.GetConnectionString(_connectionId));
        DbName = _configuration["DatabaseName"];
        _database = Client.GetDatabase(DbName);
        AreaCollection = _database.GetCollection<AreaModel>(SNACollectionName);
        BiomeCollection = _database.GetCollection<Biome>(BiomeCollectionName);
        CommentCollection = _database.GetCollection<CommentModel>(CommentCollectionName);
    }

}
