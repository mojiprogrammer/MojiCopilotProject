using DanaCopilot.Domain.Entities;
using DanaCopilot.Domain.Interfaces.Command.Base;

namespace DanaCopilot.Domain.Interfaces.Command
{
    public interface IUserCommandRepository : ICommandRepository<UserComEntity>
    {
      
    }
}
