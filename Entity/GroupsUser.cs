using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.Entity;

[Table(name:"groups_user")]
[PrimaryKey(nameof(GroupId), nameof(UserId))]
public partial class GroupsUser
{
    
    [Required]
    [Column(name:"group_id")]
    public int GroupId { get; set; }

    
    [Required]
    [Column(name:"user_id")]
    public string UserId { get; set; } = null!;

    public Group Group { get; set; } = null!;
}
