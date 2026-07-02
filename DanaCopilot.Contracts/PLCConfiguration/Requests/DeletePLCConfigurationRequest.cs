using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfiguration.Requests
{
    public sealed class DeletePLCConfigurationRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
