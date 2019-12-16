using System;
using System.Collections.Generic;

namespace OpenTraumaRegistry.Data.Models
{
    public class Flag
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        //FK
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int FlagTypeId { get; set; }
        public FlagType FlagType {get; set;}

        public List<FlagReminder> FlagReminders { get; set; }

    }
}
