using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfiguration.Requests
{
    public sealed class CreatePLCConfigurationRequest
    {
        public long PLCId { get; set; }

        public long PLCConfigurationDefinitionId { get; set; }

        public string? ConfigurationValue { get; set; }

        public long CreatedBy { get; set; }
    }
}
