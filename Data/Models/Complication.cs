using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaRegistry.Data.Models
{
    public class Complication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Note { get; set; }
 
        //FK
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
      
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
