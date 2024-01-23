using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.Models;

public partial class GAME_PLATFORM
{
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("platform_id")]
    public string? PlatformId { get; set; }

    [Column("game_id")]
    public string? GameId { get; set; }

    public virtual GAME? Game { get; set; }

}
