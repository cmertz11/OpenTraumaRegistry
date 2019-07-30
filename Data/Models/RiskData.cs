using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Data.Models
{
    public class RiskData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int RefRiskDataId { get; set; }
        public RefRiskData RefRiskData { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
