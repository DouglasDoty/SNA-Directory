using MongoDB.Driver;
namespace SNA_Directory.Data.DataAccess;

public class BiomeData : IBiomeData
{
    private readonly IMongoCollection<Biome> _Biomes;
    public bool DatabaseInvalid { get; private set; }

    public BiomeData(IDbConnection db)
    {
        _Biomes = db.BiomeCollection;
        try
        {
            if (_Biomes.EstimatedDocumentCount() == 0)
            {
                InitializeBiomeDatabase();
            }
            DatabaseInvalid = false;
        }
        catch (Exception)
        {
            DatabaseInvalid = true;
        }

    }

    public async Task<List<Biome>> GetAllBiomesAsync()
    {
        if (DatabaseInvalid) return null;
        var results = await _Biomes.FindAsync(_ => true);
        return results.ToList();
    }


    private void InitializeBiomeDatabase()
    {
        List<Biome> biomes = new()
        {
            new Biome
            {
                Name = "Coniferous Forest",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/lmf/photo_212ma.jpg",
                Landscapes = new List<string> { "Agassiz Lowlands",
                    "Border Lakes",
                    "Chippewa Plains",
                    "Laurentian Uplands",
                    "Littlefork - Vermillion Uplands",
                    "Mille Lacs Uplands",
                    "North Shore Highlands",
                    "Pine Moraines and Outwash Plains",
                    "St.Louis Moraines",
                    "Tamarack Lowlands"}
            },
            new Biome
            {
                Name = "Deciduous Woods",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/ebf/photo_222mb.jpg",
                Landscapes = new List<string>{"Anoka Sand Plain",
                        "Big Woods",
                        "Hardwoood Hills",
                        "Oak Savanna",
                        "Rochester Plateau",
                        "St. Paul-Baldwin Plains and Moraines",
                        "The Blufflands" }
            },
            new Biome
            {
                Name = "Prairie Grasslands",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/ppa/photo_251bb.jpg",
                Landscapes = new List<string> { "Coteau Moraines", "Inner Coteau", "Minnesota River Prairie", "Red River Prairie" }
            },
            new Biome
            {
                Name = "Tallgrass Aspen Parklands",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/tap/photo_223na.jpg",
                Landscapes = new List<string> { "Aspen Parklands" }
            }
        };

        _Biomes.InsertMany(biomes);

    }
}
