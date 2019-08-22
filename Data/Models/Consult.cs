using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Data.Models
{
    public class Consult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int ConsultPhysicianId{ get; set; }
        [ForeignKey("ConsultPhysicianId")]
        public RefPhysician ConsultPhysician { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
