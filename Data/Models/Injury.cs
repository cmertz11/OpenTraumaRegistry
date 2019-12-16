using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{
    public class Injury
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        // Abreviated Injury Scale
        public string AISCode { get; set; }

        public string Diagnosis { get; set; }

        //FK to ICD 10 table
        public int ICD10 { get; set; }

        // Injury Severity Score 1-75
        public int ISS { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
