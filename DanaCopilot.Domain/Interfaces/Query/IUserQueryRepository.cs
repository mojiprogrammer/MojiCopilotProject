using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Interfaces.Query
{
    public interface IUserQueryRepository
    {
        Task<UserComEntity> GetUserAsync(int mobileNo);
    }
}
