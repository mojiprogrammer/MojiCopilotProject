using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DanaCopilot.Application.Commands.User
{
    public record UserCommand : IRequest<bool>
    {
        public required string Name { get; set; }
    }
}
