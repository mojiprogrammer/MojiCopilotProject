using DanaCopilot.Application.Modules.Oee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Oee.Interfaces
{
    public interface IOeeCalculationService
    {
        Task<OeeResult> CalculateAsync(long plcId, DateTime from, DateTime to);
    }
}
