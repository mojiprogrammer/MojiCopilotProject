using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfigurationDefinition.Requests
{
    public sealed class UpdatePLCConfigurationDefinitionRequest
    {
        public long Id { get; set; }

        public long PLCTypeId { get; set; }

        public string ConfigurationKey { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public long ConfigurationDataTypeId { get; set; }

        public string? DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public int DisplayOrder { get; set; }

        public string? ValidationRegex { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public string? EnumSource { get; set; }

        public string? Placeholder { get; set; }

        public string? HelpText { get; set; }

        public string? Description { get; set; }

        public long ModifiedBy { get; set; }
    }
}
