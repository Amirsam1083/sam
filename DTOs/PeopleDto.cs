namespace api.DTOs;

public record PeopleDto(
    string Id,
    string CodeMelli,
    string? Email
);
// {
//     public static implicit operator PeopleDto?(RegisterDto? v)
//     {
//         throw new NotImplementedException();
//     }
// }

