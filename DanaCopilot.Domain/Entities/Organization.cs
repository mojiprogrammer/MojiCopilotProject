using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain    
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
