using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.data;

public sealed class State
{
    [Key] public int StateID { get; set; }

    public required string StateName { get; set; }

    [Column("StateCode")] public required string Code { get; set; }
    public bool IsActive { get; set; }
}