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

        [MaxLength(200)]
        public string PositionInVehicle { get; set; }

        public string LocationOfOccuranceDescription { get; set; }
        public string OccuranceZipCode { get; set; }
        public DateTime InjuryDateTime { get; set; }

        [MaxLength(1000)]
        public string InjuryDetailsNarrative { get; set; }
 
        public int ArrivedFromId { get; set; }

        [ForeignKey("ArrivedFromId")]
        public RefArrivedFrom ArrivedFrom { get; set; }

        public int TransportId { get; set; }
        [ForeignKey("TransportId")]
        public RefTransport Transport { get; set; }

        public int AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        public RefAgency AgencyPreHospital { get; set; }

        public DateTime AgencyDispatchDateTime { get; set; }

        public DateTime AgencyArriveSceneDateTime { get; set;}

        public DateTime AgencyDepartSceneDateTime { get; set; }

        public DateTime AgencySceneDateTime { get; set; }

        public List<InjuryTypes> InjuryTypes { get; set; } = new List<InjuryTypes>();
        public List<SafetyDevices> SafetyDevices { get; set; } = new List<SafetyDevices>();
        public List<Vitals> Vitals { get; set; } = new List<Vitals>();

        public List<Injury> Injuries { get; set; } = new List<Injury>();

        public List<Procedure> Procedures { get; set; } = new List<Procedure>();

        public List<Complication> Complications { get; set; } = new List<Complication>();

        public List<RiskData> Risks { get; set; } = new List<RiskData>();

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
