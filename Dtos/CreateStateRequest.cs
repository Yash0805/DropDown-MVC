using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Dtos;

public sealed class CreateStateRequest
{
    [Required] public required string StateName { get; init; }
    [Required] public required string Code { get; init; }
}