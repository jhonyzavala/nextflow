using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.DTOs
{
    public class ParticipantDTO
    {   
        public string ParticipantId { get; set; }
        
        public int TaskId { get; set; }
    
    }
    
}
