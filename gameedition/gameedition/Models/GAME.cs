using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game.edition.api.Models;

public partial class GAME
{
     [Column("id"), Key]
     public int Id { get; set; }

     [Column("title")]
     public string? Title { get; set; }

     [Column("release_date")]
     public DateTime? ReleaseDate { get; set; }

     [Column("genre")]
     public string? Genre { get; set; }

     [Column("price")]
     public int? Price { get; set; }

     [Column("company_id")]
     public int? CompanyId { get; set; }

     [Column("platform_id")]
     public int? PlatformId { get; set; }

     [Column("created_at")]
     public DateTime? CreatedAt { get; set; }

     [Column("created_by")]
     public string? CreatedBy { get; set; }

     [Column("updated_at")]
     public DateTime? UpdatedAt { get; set; }

     [Column("updated_by")]
     public string? UpdatedBy { get; set; }

     public virtual GAME_PLATFORM? Platform { get; set; }

   
}
