using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaRegistry.Data.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(1)]
        public string MI { get; set; }
        public DateTime DOB { get; set; }

        public int GenderReferenceId { get; set; }

        public int RaceReferenceId { get; set; }


        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }


        // Intending to link to asp identity table
        public string LastUpdatedBy { get; set; }

        [MaxLength(11)]
        public string SSN { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
