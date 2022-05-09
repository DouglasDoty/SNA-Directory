using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SNA_Directory.Data.Models;

public class SNAModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public int DNRId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Biome { get; set; } = string.Empty;
    public string Landscape { get; set; } = string.Empty;
}
