using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.PLCConfiguration.Responses
{
    public sealed class PLCRuntimeConfigurationResponse
    {
        public long PLCId { get; set; }

        public string PLCCode { get; set; } = string.Empty;

        public string PLCName { get; set; } = string.Empty;

        public string PLCTypeCode { get; set; } = string.Empty;

        public string PLCTypeName { get; set; } = string.Empty;

        public string IPAddress { get; set; } = string.Empty;

        public int Port { get; set; }

        public string ConfigurationKey { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string DataType { get; set; } = string.Empty;

        public string DataTypeName { get; set; } = string.Empty;

        public string? ConfigurationValue { get; set; }

        public string? DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public string? ValidationRegex { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public string? EnumSource { get; set; }
    }
}
