using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi_nextflow.Entity
{
    [Table(name:"collaborators")]
    [PrimaryKey(nameof(CollaboratorId), nameof(TaskId))]
    public class Collaborator
    {        
        [Column(name:"collaborator_id")]
        public string CollaboratorId { get; set; }
        
        [Column(name: "task_id")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }                
    }
}
