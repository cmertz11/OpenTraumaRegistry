using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OpenTraumaRegistry.Data.Models;

namespace OpenTraumaRegistry.Data.Models
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
