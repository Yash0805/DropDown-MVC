using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models;

public class CityViewModel
{
    public int CityID { get; set; }

    [Required] public string? CityName { get; set; }

    [Required] [ForeignKey("State")] public int? StateID { get; set; }

    public IEnumerable<StateViewModel>? States { get; set; }
}