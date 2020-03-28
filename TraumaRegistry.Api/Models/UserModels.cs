using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Api.Models
{
    public class UserModels
    {
        public class _dtoUser
        {
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
            public List<_UserFacility> userFacilities { get; set; } = new List<_UserFacility>();
        }
        public class _UserFacility
        {
            public int FacilityId { get; set; }
            public string FacilityName { get; set; }
            public bool Administrator { get; set; }
        }
    }
}
