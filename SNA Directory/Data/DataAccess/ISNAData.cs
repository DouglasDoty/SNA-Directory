
namespace SNA_Directory.Data.DataAccess;

public interface ISNAData
{
    Task CreateSNA(SNAModel sna);
    Task CreateMultipleSNAs(IEnumerable<SNAModel> snas);
    Task<List<SNAModel>> GetAllSNAsAsync();
    Task<SNAModel> GetSnaAsync(string id);
    Task<List<SNAModel>> GetProvinceSNAsAsync(string province);
    Task<List<Biome>> GetAllBiomesAsync();
    Task UpdateSNA(SNAModel sna);
    Task<List<CommentModel>> GetComments(int snaId);
    Task CreateComment(CommentModel comment);
}