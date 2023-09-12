using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs
{

    public class TaskDTO
    {
        public int Id { get; set; } 
        public int ApprovalTypeId { get; set; }
        public int TimeLimit { get; set; }
        public string TimeLimitType { get; set; }
        public int ParticipantTypeId { get; set; }
        public int? GroupId { get; set; }        
        public string SpecificUser { get; set; }        
        public int SpecificStatusId { get; set; }
        
    }
}
