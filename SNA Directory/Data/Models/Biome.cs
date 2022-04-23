using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SNA_Directory.Data.Models;

public class Biome
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public List<string> Landscapes { get; set; }  

}
