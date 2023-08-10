using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"transition")]
public partial class Transition
{
    
    [Column(name:"id")]    
    public int Id { get; set; }

    [Required]
    [Column(name:"current_item")]    
    public int CurrentItem { get; set; }

    [Required]
    [Column(name:"next_item")]    
    public int NextItem { get; set; }

    [Required]
    [Column(name:"event_id")]    
    public int EventId { get; set; }

    public virtual Item CurrentItemNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Item NextItemNavigation { get; set; } = null!;

}
