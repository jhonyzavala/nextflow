using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.Entity
{
    [Table(name:"participants")]
    [PrimaryKey(nameof(ParticipantId), nameof(TaskId))]
    public class Participant
    {        
        [Column(name:"participant_id")]
        public string ParticipantId { get; set; }

        [Column(name: "task_id")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }                
    }
    
}
