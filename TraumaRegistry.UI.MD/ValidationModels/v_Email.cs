using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.UI.MD.ValidationModels
{
    public class v_Email
    {
        [Required(ErrorMessage = "Email Address is Required")]
        public string NewEmailAddress { get; set; } = "";
        public bool EmailValid { get; set; } = false;
        public bool EmailFormatValid { get; set; } = false;
        public bool VerifyEmailModalOpen { get; set; } = false;
        public string EmailValidationMessage { get; set; }
    }
}
