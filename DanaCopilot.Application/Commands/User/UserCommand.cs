using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DanaCopilot.Application.Commands.User
{
    public record UserCommand : IRequest<bool>
    {
        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
    }
}
