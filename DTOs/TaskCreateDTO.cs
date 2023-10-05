using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs
{

    public class TaskCreateDTO
    {

        [Required] 
        public int Id { get; set; }

        [Required] 
        public int ApprovalTypeId { get; set; }

        [Required]              
        public int TimeLimit { get; set; }

        [Required]      
        [StringLength(maximumLength: 1)]        
        public string TimeLimitType { get; set; }
        //[time_limit_type] char (1) NOT NULL CHECK([time_limit_type] IN ('m', 'h', 'd')),

        [Required]        
        public int ParticipantTypeId { get; set; }

        public int? GroupId { get; set; }
        
        [StringLength(maximumLength: 128)]
        public string SpecificUser { get; set; }
        
        public int SpecificStatusId { get; set; }

    }
}
