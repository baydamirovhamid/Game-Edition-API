using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace game.payment.service.Models
{
    public class USER_ROLE: IdentityRole<int>
    {
        [Column("CREATED_AT")]
        public DateTime? CreatedAt { get; set; }

        [Column("CREATED_BY")]
        public string? CreatedBy { get; set; }

        [Column("UPDATED_AT")]
        public DateTime? UpdatedAt { get; set; }

        [Column("UPDATED_BY")]
        public string? UpdatedBy { get; set; }
    }
}
