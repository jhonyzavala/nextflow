using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi_nextflow.Validations;

namespace webapi_nextflow.DTOs
{

    public class ItemDTO
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkflowId { get; set; }  
        public int StageId { get; set; }
        public string Hastags { get; set; }
        public int Order { get; set; }                
    }
}
