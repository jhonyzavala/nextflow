using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"approval_type")]
public partial class ApprovalType
{
    
    [Required]
    [Column(name:"id")]
    public int Id { get; set; }

    [Required]
    [Column(name:"name")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(name:"description")]
    public string Description { get; set; } = null!;

    [Required]
    [Column(name:"order")]
    public int Order { get; set; }

    [Required]
    [Column(name:"workflow_id")]
    public string WorkflowId { get; set; } = null!;

    public virtual List<GroupsUser> GroupsUsers { get; set; }

    public virtual List<Task> Tasks { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;
}
