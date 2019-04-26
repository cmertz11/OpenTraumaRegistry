using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaRegistry.Data.Models
{
    public class Event
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime InjuryDateTime { get; set; }
      
        //FK
        public List<Vitals> Vitals { get; set; }

        public List<Injury> Injuries { get; set; }

        public List<Procedure> Procedures { get; set; }

        public List<Complication> Complications { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
