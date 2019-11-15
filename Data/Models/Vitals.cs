using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Data
{
    public class Vitals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //FK -> Not configured needs to point to RefLocation
        public decimal? Location { get; set; }
        public decimal? Systolic { get; set; }
        public decimal? Diastolic { get; set; }
        public decimal? Pulse { get; set; }
        public decimal? RespiratoryRate { get; set; }
        public decimal? SPO2 { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal? Temperature { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string Note { get; set; }
        public DateTime? TimeTaken { get; set; }
        public DateTime TimeStamp { get; set; }
        //FK
        public int EventId { get; set; } 
        public Event Event { get; set; }
    }
}
