﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTraumaRegistry.Data.Models
{

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string OfficePhone { get; set; }
        public string CellPhone { get; set; }
        public string Password { get; set; }

        public DateTime PasswordExpires { get; set; }

        public bool ForcePasswordReset { get; set; }

        public int LoginAttempts { get; set; }

        public bool Locked { get; set; }

        public bool EmailConfirmed { get; set; }

        public string ConfirmationToken { get; set; }

        public DateTime ConfirmationTokenExpires { get; set; }

        public bool SystemAdministrator { get; set; }

        public DateTime LastUpdate { get; set; }

        public int LastUpdatedBy { get; set; }

        public DateTime LastLogin { get; set; }
        [NotMapped]
        public bool Authenticated { get; set; }
        [NotMapped]
        public string jsonToken { get; set; }
        [NotMapped]
        public string message { get; set; }
    }
}
