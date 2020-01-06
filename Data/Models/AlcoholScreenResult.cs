using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{
    public class AlcoholScreenResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public decimal BAC { get; set; }
        public DateTime? TimeTaken { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
