using DanaCopilot.Application.Commands.Auth;
using DanaCopilot.Domain.DTOs;
using DanaCopilot.Domain.Interfaces.Command;
using MediatR;

namespace DanaCopilot.Application.Handlers.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository)
        {
            _otpRedisRepository = otpRedisRepository;

        }
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            await _otpRedisRepository.Insert(new Otp { UserId = 1, OtpCode = "3265", IsUse = false });
            //TODO main logic
            return true;
        }
    }
}
