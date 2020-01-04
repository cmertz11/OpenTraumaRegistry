using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OpenTraumaRegistry.Data.Models
{
    public class HomeResidence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int RefHomeResidenceId { get; set; }

        [ForeignKey("RefHomeResidenceId")]
        public RefHomeResidence RefHomeResidence { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
