
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs;

public class WorkflowDTO
{

    public string Id { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }

}

