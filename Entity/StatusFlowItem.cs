using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"status_flow_items")]
public partial class StatusFlowItem
{
    
    [Column(name: "id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [Column(name: "name")]
    [StringLength(maximumLength:150)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(name: "description")]    
    [StringLength(maximumLength:300)]
    public string Description { get; set; } = null!;

    [Column(name: "task")]    
    public bool? Task { get; set; }

    [Column(name: "approval")]    
    public bool? Approval { get; set; }

    public virtual List<WorkFlowItem> WorkFlowItems { get; set; } 
    
}
