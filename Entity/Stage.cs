using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity
{
    [Table(name: "stages")]
    public class Stage
    {
        [Column(name: "id")]
        public int Id{ get; set; }

        [Required]
        [Column(name: "name")]
        public string Name { get; set; }

        [Required]
        [Column(name: "description")]
        public string Description { get; set; }

        [Required]
        [Column(name: "order")]
        public int Order { get; set; }

        [Required]
        [StringLength(maximumLength: 128)]
        [Column(name: "workflow_id")]
        public string WorkflowId { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual Workflow Workflow { get; set; }

    }
}
