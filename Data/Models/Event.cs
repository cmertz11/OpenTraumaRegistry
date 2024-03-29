﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpenTraumaRegistry.Data.Models
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
       

        [MaxLength(1000)]
        public string InjuryDetailsNarrative { get; set; }

        public bool TimeInERHolder { get; set; }
        public bool FastExam { get; set; }

        public bool FastExamPositive { get; set; }

        public string BloodProducts { get; set; }

        #region Key Event DateTimes
            public DateTime? InjuryDateTime { get; set; }
            public DateTime? AgencyDispatchDateTime { get; set; }
            public DateTime? AgencyArriveSceneDateTime { get; set; }
            public DateTime? AgencyDepartSceneDateTime { get; set; }
            public DateTime? AgencySceneDateTime { get; set; }
            public DateTime? EDArrivalDateTime { get; set; }
            public DateTime? EDDischargeDateTime { get; set; }
            public DateTime? EDDispoEDOrderTime { get; set; }
            public DateTime? ActivationDateTime { get; set; }
            public DateTime? HospitalDischargeOrder { get; set; }
            public DateTime? AnesthesiaArrivalTime { get; set; }
            public DateTime? BloodProductsStartTime{ get; set; }
            public DateTime? BloodProductsStopTime { get; set; }
        #endregion

        #region ForeignKey Id's Single

            [ForeignKey("OutcomeId")]
            public int? OutcomeId { get; set; }
            public RefOutcome Outcome { get; set; }
            public int? TraumaLevelId { get; set; }

            [ForeignKey("TraumaLevelId")]
            public RefTraumaLevel TraumaLevel { get; set; }

            public int ArrivedFromId { get; set; }
            [ForeignKey("ArrivedFromId")]
            public RefArrivedFrom ArrivedFrom { get; set; }

            public int TransportId { get; set; }
            [ForeignKey("TransportId")]
            public RefTransport Transport { get; set; }

            public int? TransportModeId { get; set; }
            [ForeignKey("TransportModeId")]
            public RefTransportMode TransportMode { get; set; }

            public int? AgencyPreHospitalId { get; set; }
            [ForeignKey("AgencyPreHospitalId")]
            public RefAgency AgencyPreHospital { get; set; }

            public int? ERPhysicianId { get; set; }
            [ForeignKey("ERPhysicianId")]
            public RefPhysician ERPhysician { get; set; }
            public int? AdmitPhysicianId { get; set; }
            [ForeignKey("AdmitPhysicianId")]
            public RefPhysician AdmitPhysician { get; set; }

            public int? ChildSpecificRestraintId { get; set; }
            [ForeignKey("ChildSpecificRestraintId")]
            public RefChildSpecificRestraint ChildSpecificRestraint { get; set; }
        #endregion

        public List<DrugScreen> DrugScreens { get; set; } = new List<DrugScreen>();
        public List<InjuryTypes> InjuryTypes { get; set; } = new List<InjuryTypes>();
        public List<SafetyDevices> SafetyDevices { get; set; } = new List<SafetyDevices>();

        public List<HomeResidence> HomeResidences { get; set; } = new List<HomeResidence>();
        public List<Vitals> Vitals { get; set; } = new List<Vitals>();

        public List<Injury> Injuries { get; set; } = new List<Injury>();

        public List<Procedure> Procedures { get; set; } = new List<Procedure>();

        public List<Complication> Complications { get; set; } = new List<Complication>();

        public List<RiskData> Risks { get; set; } = new List<RiskData>();

        public List<Consult> Consults { get; set; } = new List<Consult>();

        public List<AlcoholScreenResult> AlcoholScreenResults { get; set; } = new List<AlcoholScreenResult>();

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
