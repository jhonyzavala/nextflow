using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi_nextflow.Validations;

namespace webapi_nextflow.Entity
{
    [Table(name: "items")]
    public class Item
    {
        [Column(name: "id")]
        public int Id { get; set; }
        
        // [FirstCapitalLetter]
        [Required]
        [StringLength(maximumLength: 700)]
        [Column(name:"name")]        
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        [Column(name: "description")]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 128)]
        [Column(name: "workflow_id")]
        public string WorkflowId { get; set; }
  

        [Required]
        [Column(name: "stage_id")]
        public int StageId { get; set; }

        [Column(name: "hastags")]
        public string Hastags { get; set; }

        [Required]
        [Column(name: "order")]
        public int Order { get; set; }

        [NotMapped]
        public int Minor { get; set; }

        [NotMapped]
        public int Greater { get; set; }
        
        // start navigation property        
        public Workflow Workflow { get; set; }
        public Stage Stage { get; set; }
        // end navigation property

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name.ToString()[0].ToString();

                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("The first letter must be uppercase", 
                        new string[] {nameof(Name)} );
                }
            }

            if (Minor > Greater)
            {
                yield return new ValidationResult("This value cannot be larger than the Greater field",
                       new string[] { nameof(Minor) });
            }

        }
    }
}
