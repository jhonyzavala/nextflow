using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.DTOs
{
    public class StageCreateDTO
    {
        [Column(name: "id")]
        public int Id{ get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [StringLength(maximumLength: 128)]
        public string WorkflowId { get; set; }

    }
}
