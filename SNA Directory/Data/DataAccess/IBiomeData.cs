
namespace SNA_Directory.Data.DataAccess;

public interface IBiomeData
{
    bool DatabaseInvalid { get; }

    Task<List<Biome>> GetAllBiomesAsync();
}