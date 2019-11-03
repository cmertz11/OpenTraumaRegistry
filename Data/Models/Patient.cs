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

        public string MRN { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(1)]
        public string MI { get; set; }
        public DateTime? DOB { get; set; }

        public string email { get; set; }
        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }


        // Intending to link to asp identity table
        public string LastUpdatedBy { get; set; }

        public int GenderReferenceId { get; set; }

        [ForeignKey("GenderReferenceId")]
        public RefGender Gender { get; set; }

        public int RaceReferenceId { get; set; }

        [ForeignKey("RaceReferenceId")]
        public RefRace Race { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
