using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs;

public class GroupsUserCreateDTO
{    
    [Required]
    public int GroupId { get; set; }
    
    [Required]
    public string UserId { get; set; } = null!;

}
