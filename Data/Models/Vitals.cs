using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaData.Models
{
    public class Vitals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public string EventId { get; set; }
        //FK
        public string PatientId { get; set; }

        //FK -> ReferenceDetail Where Reference.Name = "Location"
        public int Location { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public int Pulse { get; set; }
        public int RespiratoryRate { get; set; }
        public int SPO2 { get; set; }
        public string Temperature { get; set; }

        public string Note { get; set; }
        public DateTime TimeTaken { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
