using MediatR;

namespace DanaCopilot.Application.Queries.Auth
{
    public class AuthQuery : IRequest<bool>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public required string MobileNo { get; set; }
    }
}
