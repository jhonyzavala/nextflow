using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"groups")]
public class Group
{
    
    [Column(name:"id")]
    public int Id { get; set; }

    [Required]
    [Column(name:"name")]
    [StringLength(maximumLength:150)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(name:"description")]
    [StringLength(maximumLength:300)]
    public string Description { get; set; } = null!;

    [Required]
    [Column(name:"order")]
    public int Order { get; set; }

    [Required]
    [Column(name:"workflow_id")]
    [StringLength(maximumLength:128)]
    public string WorkflowId { get; set; } = null!;

    public virtual List<GroupsUser> GroupsUsers { get; set; }

    public virtual List<Task> Tasks { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;
}
