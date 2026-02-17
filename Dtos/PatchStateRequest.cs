using System.ComponentModel.DataAnnotations;
namespace WebApplication6.Dtos
{
    public class PatchStateRequest
    {
        public required bool IsActive { get; init; }
    }
}
