using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SNA_Directory.Data.Models;

public class CommentModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int DnrId { get; set; }
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(20, ErrorMessage ="Name can not be more than 20 characters.")]
    public string Name { get; set; }
    public DateTime Date { get; set; }
    [Required(AllowEmptyStrings =false, ErrorMessage = "Please enter a comment or press Cancel.")]
    [StringLength (500, ErrorMessage = "Comments are limited to 500 characters.")]
    public string Text { get; set; }
}
