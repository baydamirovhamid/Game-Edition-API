using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.Models;

public partial class GAME_COMPANY
{    
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("game_id")]
    public int? GameId { get; set; }

    public virtual GAME? Game { get; set; }
}
