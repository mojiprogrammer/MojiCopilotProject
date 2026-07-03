using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ShiftCalendar.Requests
{
    public sealed class UpdateShiftCalendarRequest
    {
        public long Id { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsWorkingDay { get; set; }

        public string? Notes { get; set; }

        public long ModifiedBy { get; set; }
    }
}
