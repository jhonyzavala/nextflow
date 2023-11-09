using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public partial class WorkFlowItemDTO
{
    
    public string Id { get; set; } = null!;    
    public int ItemId { get; set; }
    public string UserId { get; set; }    
    public string Object { get; set; } = null!;    
    public DateTime StarDate { get; set; }    
    public DateTime? EndDate { get; set; }    
    public int StatusItemFlowId { get; set; }    
    public bool? EvaluatedVote { get; set; }    
    public string VotingId { get; set; }    
    public string Comment { get; set; }
}
