using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"approvaltype")]
public partial class ApprovalType
{
    
    [Required]
    [Column(name:"id")]
    public int Id { get; set; }

    [Required]
    [Column(name:"name")]
    [StringLength(maximumLength:150)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(name:"name")]
    [StringLength(maximumLength:300)]
    public string Description { get; set; } = null!;

    public int Order { get; set; }

    public List<Task> Tasks { get; set; } 
}
