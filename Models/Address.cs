using System.ComponentModel.DataAnnotations;

namespace api.Models;

public record Address(
    [MinLength(3), MaxLength(25)] string City,
    [MinLength(2), MaxLength(60)] string Street,
    [MinLength(3), MaxLength(30)] string State,
    [MinLength(3), MaxLength(25)] string EsmeSakhteman,
    string Pelak,
    [MinLength(10), MaxLength(10)] string CodePosti
);
