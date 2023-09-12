using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi_nextflow.Validations;

namespace webapi_nextflow.DTOs
{

    public class ItemCreateDTO
    {        
        [Required]
        [StringLength(maximumLength: 700)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]        
        public string Description { get; set; }          
        [Required]   
        public int StageId { get; set; }    
        public string Hastags { get; set; }        
        [Required]
        public int Order { get; set; }
       // public virtual Task Task { get; set; }
    }
}
