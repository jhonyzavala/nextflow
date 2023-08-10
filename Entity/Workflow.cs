
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_nextflow.Entity
{

    [Table(name:"workflows")]
    public class Workflow
    {
        [StringLength(maximumLength:128, ErrorMessage = "The {0} field cannot exceed {1} characters")]
        [Column(name: "id")]
        public string Id { get; set; }

        [Required(ErrorMessage ="The {0} field is required")]
        [StringLength(maximumLength:350, ErrorMessage = "The {0} field cannot exceed {1} characters")]
        [Column(name: "description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [Column(name: "owner")]
        public string Owner { get; set; }

        public List<Item> Items { get; set; }

    }
}
