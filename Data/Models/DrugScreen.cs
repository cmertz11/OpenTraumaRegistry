using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenTraumaRegistry.Data.Models;


namespace OpenTraumaRegistry.Data.Models
{
    public class DrugScreen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? TimeTaken { get; set; }

        public List<DrugScreenSubstances> DrugScreenSubstances = new List<DrugScreenSubstances>();
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
