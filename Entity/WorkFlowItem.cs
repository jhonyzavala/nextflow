using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"work_flow_items")]
public partial class WorkFlowItem
{
    
    [Column(name:"id")]
    public string Id { get; set; } = null!;

    [Required]
    [Column(name:"task_id")]
    public int TaskId { get; set; }

    public string UserId { get; set; }

    [Required]
    [Column(name:"object")]    
    public string Object { get; set; } = null!;

    [Required]
    [Column(name:"star_date")]
    public DateTime StarDate { get; set; }

    [Column(name:"end_date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Column(name:"status_item_flow_id")]
    public int StatusItemFlowId { get; set; }

    [Column(name:"evaluated_vote")]
    public bool? EvaluatedVote { get; set; }

    [Column(name:"voting_id")]
    [StringLength(maximumLength:128)]
    public string VotingId { get; set; }

    [Column(name:"comment")]
    [StringLength(maximumLength:3000)]
    public string Comment { get; set; }

    public virtual StatusFlowItem StatusItemFlow { get; set; } = null!;

    public virtual Voting Voting { get; set; }

    public virtual Task Task { get; set; }
     

}
