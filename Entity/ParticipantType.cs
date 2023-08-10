using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity;

[Table(name:"participanttype")]
public partial class ParticipantType
{
    
    [Required]
    [Column(name:"id")]
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength:150)]
    [Column(name:"name")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(maximumLength:300)]
    [Column(name:"description")]
    public string Description { get; set; } = null!;

    [Column(name:"order")]
    public int Order { get; set; }

    public List<Task> Tasks { get; set; } 
}
