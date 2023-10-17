using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs
{
  
    public class CollaboratorDTO
    {        
  
        public string CollaboratorId { get; set; }        
        public int TaskId { get; set; }

    }
}
