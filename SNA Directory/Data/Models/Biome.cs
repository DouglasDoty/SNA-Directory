using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SNA_Directory.Data.Models;

public class Biome
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;  
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public List<string> Landscapes { get; set; } = new();  

}
