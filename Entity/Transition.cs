using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.Entity;

[Table(name:"transition")]
[Index(nameof(CurrentItem), nameof(NextItem), nameof(EventId), IsUnique =true )]
public partial class Transition
{
    
    [Column(name:"id")]    
    public int Id { get; set; }

    [Required]
    [Column(name:"current_item")]    
    [ForeignKey("CurrentItemNavigation")]
    public int CurrentItem { get; set; }

    [Required]
    [Column(name:"next_item")]    
    [ForeignKey("NextItemNavigation")]
     public int NextItem { get; set; }

    [Required]
    [Column(name:"event_id")]    
    public int EventId { get; set; }

    public virtual Item CurrentItemNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Item NextItemNavigation { get; set; } = null!;

}
