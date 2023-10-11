using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs;

public class TransitionCreateDTO
{    
   
    [Required]
    public int CurrentItem { get; set; }

    [Required]
     public int NextItem { get; set; }

    [Required]
    public int EventId { get; set; }

}
