﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{
    public class SafetyDevices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //FK
        public int RefSafetyDeviceId { get; set; }

        [ForeignKey("RefSafetyDeviceId")]
        public RefProtectiveDevice RefProtectiveDevice { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
