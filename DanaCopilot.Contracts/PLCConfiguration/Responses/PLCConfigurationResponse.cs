using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfiguration.Responses
{
    public sealed class PLCConfigurationResponse
    {
        public long Id { get; set; }

        public long PLCId { get; set; }

        public string PLCName { get; set; } = string.Empty;

        public long PLCConfigurationDefinitionId { get; set; }

        public string ConfigurationKey { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string DataType { get; set; } = string.Empty;

        public string? ConfigurationValue { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
