using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SNA_Directory.Data.Models;

public class CommentModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;  
    public int DnrId { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name.")]
    [StringLength(20, ErrorMessage ="Name can not be more than 20 characters.")]
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    [Required(AllowEmptyStrings =false, ErrorMessage = "Please enter a comment or press Discard Comment.")]
    [StringLength (256, ErrorMessage = "Comments are limited to 256 characters.")]
    public string Text { get; set; } = string.Empty ;
}
