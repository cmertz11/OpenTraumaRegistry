using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpenTraumaRegistry.Data.Models
{
    public class InjuryTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int RefInjuryTypeId { get; set; }

        [ForeignKey("RefInjuryTypeId ")]
        public RefInjuryType RefInjuryType { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
