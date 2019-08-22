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

        //FK
        public int PatientId { get; set; }

        //FK -> Not configured needs to point to RefLocation
        public int Location { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public int Pulse { get; set; }
        public int RespiratoryRate { get; set; }
        public int SPO2 { get; set; }
        public string Temperature { get; set; }

        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        public string Note { get; set; }
        public DateTime TimeTaken { get; set; }

        public DateTime TimeStamp { get; set; }
        
 //FK
        public int EventId { get; set; } 
        public Event Event { get; set; }
    }
}
