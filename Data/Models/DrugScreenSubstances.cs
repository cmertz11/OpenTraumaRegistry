
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{
    public class DrugScreenSubstances
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int SubstanceId { get; set; }
        [ForeignKey("SubstanceId")]
        public RefDrugScreen Substance { get; set; }


        public int DrugScreenId { get; set; }
        public DrugScreen DrugScreen { get; set; }
    }
}
