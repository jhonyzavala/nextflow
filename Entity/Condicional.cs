using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webapi_nextflow.Entity
{
    [Table(name:"conditional")]
    public partial class Conditional
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(name:"expression")]
        public string Expression { get; set; } = null!;

        public Item IdNavigation { get; set; } = null!;
    }
}