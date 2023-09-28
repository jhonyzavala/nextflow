using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs;

public class TransitionDTO
{    
    public int Id { get; set; }
    public int CurrentItem { get; set; }
    public int NextItem { get; set; }
    public int EventId { get; set; }
}
