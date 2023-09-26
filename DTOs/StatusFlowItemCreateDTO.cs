using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public partial class StatusFlowItemCreateDTO
{    
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength:150)]
    public string Name { get; set; } 

    [Required]
    [StringLength(maximumLength:300)]
    public string Description { get; set; }

    public bool? Task { get; set; }
    public bool? Approval { get; set; }
    
}
