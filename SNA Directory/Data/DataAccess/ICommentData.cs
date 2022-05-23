
namespace SNA_Directory.Data.DataAccess;

public interface ICommentData
{
    Task CreateComment(CommentModel comment);
    Task<List<CommentModel>> GetComments(int snaId);
}