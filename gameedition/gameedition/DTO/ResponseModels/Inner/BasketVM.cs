using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;

public partial class BasketVM
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public int? GameId { get; set; }
    public DateTime? Date { get; set; }
    public int? TotalAmount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }

}