
namespace SNA_Directory.Data.DataAccess;

public interface IAreaData
{
    Task<List<AreaModel>> GetAllAreasAsync();
    Task<AreaModel> GetAreaAsync(string id);
    Task<List<AreaModel>> GetProvinceAreasAsync(string biome);
    Task CreateArea(AreaModel sna);
    Task CreateMultipleAreas(IEnumerable<AreaModel> snas);
    Task UpdateArea(AreaModel sna);
    void InitializeDatabase();
}