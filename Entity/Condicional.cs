using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webapi_nextflow.Entity
{
    [Table(name:"conditional")]
    public class Conditional
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(name:"expression")]
        [StringLength(maximumLength:255)]
        public string Expression { get; set; } = null!;

        [ForeignKey("Id")]
        public virtual Item Item { get; set; } = null!;
    }
}