using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs
{
    public class ParticipantCreateDTO
    {        
        [Required]
        public string ParticipantId { get; set; }

        [Required]
        public int TaskId { get; set; }
    
    }
    
}
