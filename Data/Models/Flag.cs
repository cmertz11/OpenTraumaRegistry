using System;
using System.Collections.Generic;

namespace TraumaData.Models
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

        public List<FlagReminder> Reminders { get; set; }

    }
}
