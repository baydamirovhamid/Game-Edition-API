using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.DTO.ResponseModels.Inner
{
    public class CompanyVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
