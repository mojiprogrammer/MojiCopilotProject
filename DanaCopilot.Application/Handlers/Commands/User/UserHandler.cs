using DanaCopilot.Application.Commands.User;
using MediatR;

namespace DanaCopilot.Application.Handlers.Commands.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
