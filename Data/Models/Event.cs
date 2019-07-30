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
        public List<Vitals> Vitals { get; set; } = new List<Vitals>();

        public List<Injury> Injuries { get; set; } = new List<Injury>();

        public List<Procedure> Procedures { get; set; } = new List<Procedure>();

        public List<Complication> Complications { get; set; } = new List<Complication>();

        public List<RiskData> Risks { get; set; } = new List<RiskData>();

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
