using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.Models;

public partial class BASKET
{
     [Column("id"), Key]
     public int Id { get; set; }

     [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("game_id")]     
    public int? GameId { get; set; }

    [Column("date")]     
    public DateTime? Date { get; set; }

    [Column("total_amount")]     
    public int? TotalAmount { get; set; }

    [Column("created_at")]     
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]  
    public string? CreatedBy { get; set; }

    [Column("updated_at")]  
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]  
    public string? UpdatedBy { get; set; }

    [Column("is_deleted")]  
    public bool? IsDeleted { get; set; }

    public virtual GAME? Game { get; set; }
}
