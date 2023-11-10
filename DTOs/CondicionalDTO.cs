using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webapi_nextflow.DTOs
{
    public class ConditionalDTO
    {  
        public int Id { get; set; }  
        public string Expression { get; set; } = null!;

    }
}