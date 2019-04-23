﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaData.Models
{
    public class Procedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EventId { get; set; }

        public int Location { get; set; }

        public string ProcedureNote { get; set; }

        public int ICD10 { get; set; }

        public int ProviderNo { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
