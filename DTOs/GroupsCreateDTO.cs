
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public class GroupsCreateDTO{

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

}