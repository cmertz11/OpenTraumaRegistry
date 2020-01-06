using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpenTraumaRegistry.Data.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string MRN { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "First Name too long (100 character limit).")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First Name too long (100 character limit).")]
        public string LastName { get; set; }

        [MaxLength(1)]
        public string MI { get; set; }
        public DateTime? DOB { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }
        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

        [NotMapped]
        public int EventCount { get; set; }
        // Intending to link to asp identity table
        public string LastUpdatedBy { get; set; }

        public int GenderReferenceId { get; set; }

        [ForeignKey("GenderReferenceId")]
        public RefSex Gender { get; set; }

        public int RaceReferenceId { get; set; }

        [ForeignKey("RaceReferenceId")]
        public RefRace Race { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
