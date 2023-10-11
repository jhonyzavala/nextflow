using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs
{
    public class StageDTO
    {

        public int Id{ get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public int Order { get; set; }      
        public string WorkflowId { get; set; }
    }
}
