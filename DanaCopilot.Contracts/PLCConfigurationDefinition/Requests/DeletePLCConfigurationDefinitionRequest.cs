using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfigurationDefinition.Requests
{
     public sealed class DeletePLCConfigurationDefinitionRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
