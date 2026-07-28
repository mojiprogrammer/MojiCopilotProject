using AutoMapper;
using DanaCopilot.Application.Commands.Auth;
using DanaCopilot.Domain.DTOs;
using DanaCopilot.Domain.Entities;
using DanaCopilot.Domain.Interfaces.Command;
using MediatR;

namespace DanaCopilot.Application.Handlers.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IMapper _mapper;
        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository, IUserCommandRepository userCommandRepository,IMapper mapper)
        {
            _otpRedisRepository = otpRedisRepository;
            _userCommandRepository = userCommandRepository;
            _mapper = mapper;

        }
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            //try
            //{
            //    var userobj = _mapper.Map<UserComEntity>(request);
            //    //var user=await _userCommandRepository.Insert

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            await _otpRedisRepository.Insert(new Otp { UserId = 1, OtpCode = "3265", IsUse = false });
           // TODO main logic
            return true;
        }
    }
}
