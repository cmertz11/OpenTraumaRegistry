using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaRegistry.Data.Models
{
    public class Procedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Location { get; set; }

        public string ProcedureNote { get; set; }

        public int ICD10 { get; set; }

        public int ProviderNo { get; set; }

        public DateTime Timestamp { get; set; }

        //FK
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
