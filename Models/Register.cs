using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace api.Models;

public record Register(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string CodeMelli,
    [MinLength(2), MaxLength(25)] string FirstName,
    [MinLength(2), MaxLength(25)] string LastName,
    [Range(15, 99)] int Age,
    string Password,
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string? Email
);
// {
//     public static implicit operator Register?(RegisterDto? v)
//     {
//         throw new NotImplementedException();
//     }
// }


