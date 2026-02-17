using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models;

public sealed class StateViewModel
{
    [Key] public int StateID { get; init; }

    public required string StateName { get; init; }
    public required string Code { get; init; }
}