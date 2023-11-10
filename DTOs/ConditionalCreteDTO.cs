using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webapi_nextflow.DTOs
{
    public class ConditionalCreteDTO
    {
  
//        public int Id { get; set; }

        [Required]  
        public string Expression { get; set; } = null!;

    }
}