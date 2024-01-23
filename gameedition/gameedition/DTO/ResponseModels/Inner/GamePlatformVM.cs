using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;

public partial class GamePlatformVM
{
    public int Id { get; set; }
    public int? PlatformId { get; set; }
    public int? GameId { get; set; }



}
