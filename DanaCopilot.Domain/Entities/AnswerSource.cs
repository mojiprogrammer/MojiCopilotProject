using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace DanaCopilot.Domain
{
    public class AnswerSource
    {
        public long Id { get; set; }

        public long MessageId { get; set; }

        public SourceType SourceType { get; set; }

        public long ReferenceId { get; set; }
    }
}
