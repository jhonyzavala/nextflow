using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public partial class WorkFlowItemDTO
{
    
    public string Id { get; set; } 
    public int TransitionId { get; set;}
     public string Object { get; set; }
    public DateTime StarDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int StatusItemFlowId { get; set; }
    public bool? EvaluatedVote { get; set; }
    public string VotingId { get; set; }
    public string Comment { get; set; }
}
