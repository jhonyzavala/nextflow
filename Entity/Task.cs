using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity
{
    [Table(name:"task")]
    public class Task
    {
        
        [Column(name:"id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "approval_type_id")]
        public int ApprovalTypeId { get; set; }

        [Required]
        [Column(name: "time_limit")]
        public int TimeLimit { get; set; }

        [Required]
        [Column(name: "time_limit_type")]
        [StringLength(maximumLength: 1)]        
        public string TimeLimitType { get; set; }
        //[time_limit_type] char (1) NOT NULL CHECK([time_limit_type] IN ('m', 'h', 'd')),

        [Required]
        [Column(name: "participant_type_id")]
        public int ParticipantTypeId { get; set; }

        [Column(name: "group_id")]
        public int? GroupId { get; set; }

        [Column(name: "specific_user")]
        [StringLength(maximumLength: 128)]
        public string SpecificUser { get; set; }

        [Column(name: "specific_status_id")]
        public int SpecificStatusId { get; set; }

        // start navigation property     
        [ForeignKey("Id")]
        public  Item Item { get; set; } = null!;

        //approval_type_id
        //participant_type_id
        
        
        public Group Group { get; set; }

        public  SpecificStatus SpecificStatus { get; set; }

        // end navigation property     
        
    }
}
