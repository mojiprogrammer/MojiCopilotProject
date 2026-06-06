using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.BackgroundJobs.Jobs
{
    public class JobExecutionResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime FinishedAt { get; set; }
    }
}
