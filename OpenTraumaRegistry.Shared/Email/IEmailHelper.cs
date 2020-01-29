using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Shared
{
    public interface IEmailHelper
    {
        Task SendAsync(EmailObj email);
        string GenerateEmailConfirmationRequest(string firstName, string tempPassword, string confirmationToken);
 
    }
 
}
