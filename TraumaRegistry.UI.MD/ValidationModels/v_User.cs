using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.UI.MD.ValidationModels
{
    public class v_User
    {
        [Required(ErrorMessage = "Email Address is Required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Cell Phone is Required")]
        public string CellPhone { get; set; }
        public string OfficePhone { get; set; }
        public bool SystemAdministrator { get; set; }
    }
}
