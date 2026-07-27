using DanaCopilot.Application.Queries.Auth;
using MediatR;
using DanaCopilot.Auth;

namespace DanaCopilot.Application.Handlers.Queries
{
    public class AuthHandler : IRequestHandler<AuthQuery, bool>
    {
        private readonly IJwtHandler _jwtHandler;
        public AuthHandler(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }
        public async Task<bool> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            //TODO 
            var token = _jwtHandler.Create(35);
            return true;
        }
    }
}
