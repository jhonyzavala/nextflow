
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public class WorkflowCreateDTO
{

    [Required]
    [StringLength(maximumLength:128, ErrorMessage = "The {0} field cannot exceed {1} characters")]
    public string Id { get; set; }
    
    [Required]
    [StringLength(maximumLength:350, ErrorMessage = "The {0} field cannot exceed {1} characters")]
    public string Description { get; set; }

    [Required]
    public string Owner { get; set; }

}

