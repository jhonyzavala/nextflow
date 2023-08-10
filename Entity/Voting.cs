using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"voting")]
public partial class Voting
{
    [Column(name:"id")]
    [StringLength(maximumLength:128)]
    public string Id { get; set; } = null!;

    [Required]
    [Column(name:"task_id")]
    public int TaskId { get; set; }
    
    [Column(name:"activate_voting")]
    public bool? ActivateVoting { get; set; }

    [Column(name:"start_date_vote")]
    public DateTime? StartDateVote { get; set; }

    [Column(name:"end_date_vote")]
    public DateTime? EndDateVote { get; set; }

    [Column(name:"close_voting")]
    public bool? CloseVoting { get; set; }   
    public virtual List<WorkFlowItem> WorkFlowItems { get; set; }
  
}
