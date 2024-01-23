using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;

public partial class GameVM
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public int? Price { get; set; }

    public int? CompanyId { get; set; }

    public int? PlatformId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
