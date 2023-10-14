namespace api.DTOs;

public record RegisterDto(
    
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")] string Email,
    
    [DataType(DataType.Password), MinLength(7), MaxLength(20)] string Password,

    string CodeMelli,

    [MinLength(2), MaxLength(25)] string FirstName,

    [MinLength(2), MaxLength(25)] string LastName,

    [Range(15, 99)] int Age
);

public record LoginDto(
    string Email,
    string Password
);
