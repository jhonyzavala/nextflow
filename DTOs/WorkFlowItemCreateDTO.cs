using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public partial class WorkFlowItemCreateDTO
{
    
   // public string Id { get; set; } = null!;

    [Required]
    public int TransitionId { get; set; }

    [Required]
    public string Object { get; set; } = null!;

    [Required]
    public DateTime StarDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public int StatusItemFlowId { get; set; }

    public bool? EvaluatedVote { get; set; }

    [StringLength(maximumLength:128)]
    public string VotingId { get; set; }

    [StringLength(maximumLength:3000)]
    public string Comment { get; set; }

}
