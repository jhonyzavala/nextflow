
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public class GroupsCreateDTO{

    [Required]
    [StringLength(maximumLength:150)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(maximumLength:300)]
    public string Description { get; set; } = null!;

    [Required]
    public int Order { get; set; }

}