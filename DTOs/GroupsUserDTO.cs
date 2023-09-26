using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs;

public class GroupsUserDTO
{    
    public int GroupId { get; set; }    
    public string UserId { get; set; } = null!;

}
