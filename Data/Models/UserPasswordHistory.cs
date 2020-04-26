using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{
    public class UserPasswordHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
