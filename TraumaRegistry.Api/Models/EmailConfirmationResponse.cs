using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Api.Models
{
    public class EmailConfirmationResponse
    {  
        public bool Success { get; set; } = false;
        public string Token { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
