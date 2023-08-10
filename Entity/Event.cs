using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"event")]
public partial class Event
{

    [Required]
    [Column(name:"id")]
    public int Id { get; set; }

    [Required]
    [Column(name:"name")]
    [StringLength(maximumLength:100)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(name:"description")]
    [StringLength(maximumLength:150)]
    public string Description { get; set; } = null!;

    public virtual List<Transition> Transitions { get; set; } 


}
