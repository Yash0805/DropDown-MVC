using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Dtos
{
    public sealed class CreateCityRequest
    {
        [Required] public required string CityName { get; init; }
        [Required] public required int StateID { get; init; }
    }
}
