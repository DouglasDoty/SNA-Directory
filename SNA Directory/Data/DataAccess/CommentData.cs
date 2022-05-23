using MongoDB.Driver;
namespace SNA_Directory.Data.DataAccess;

public class CommentData : ICommentData
{
    private readonly IMongoCollection<CommentModel> _Comments;

    public CommentData(IDbConnection db)
    {
        _Comments = db.CommentCollection;
    }

    public async Task<List<CommentModel>> GetComments(int snaId)
    {
        var results = await _Comments.FindAsync(c => c.DnrId == snaId);
        return results.ToList().OrderByDescending(c => c.Date).ToList();
    }

    public Task CreateComment(CommentModel comment)
    {
        return _Comments.InsertOneAsync(comment);
    }

}
