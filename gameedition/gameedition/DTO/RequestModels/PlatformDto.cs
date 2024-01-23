using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.DTO.RequestModels
{
    public class PlatformDto
    {
        public string? Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

      
    }
}
