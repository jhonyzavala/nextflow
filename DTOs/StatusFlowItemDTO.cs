using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public partial class StatusFlowItemDTO
{    
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public bool? Task { get; set; }
    public bool? Approval { get; set; }    
}
