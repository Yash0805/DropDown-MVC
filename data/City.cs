using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.data
{
    public sealed class City
    {
        [Key] public int CityID { get; set; }
        [Required]
        public required string CityName { get; set; }

        [ForeignKey("State")]
        public int StateID { get; set; }

        public State? State { get; set; }
    }
}