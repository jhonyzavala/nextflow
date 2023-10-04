using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;
public class VotingDTO
{
    
    public string Id { get; set; } = null!;
    
    public int TaskId { get; set; }    
    public bool? ActivateVoting { get; set; }

    public DateTime? StartDateVote { get; set; }

    public DateTime? EndDateVote { get; set; }
    
    public bool? CloseVoting { get; set; }   
    
   // audit fields
}
