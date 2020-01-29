using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTraumaRegistry.Shared
{
    public class EmailObj
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string htmlContent { get; set; }
    }
}
