using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserLoginStatistics
    {
        public int SuccessfulLogins { get; set; }
        public int FailedLogins { get; set; }
        public DateTime? LastSuccessfulLogin { get; set; }
        public int DaysActive { get; set; }
    }
}
