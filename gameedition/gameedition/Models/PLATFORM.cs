using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace game.edition.api.Models;

public partial class PLATFORM
{
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("release_date")]
    public DateTime? ReleaseDate { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public string? UpdatedBy { get; set; }
   

}
