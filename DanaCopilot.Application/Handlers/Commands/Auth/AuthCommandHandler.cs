using AutoMapper;
using DanaCopilot.Application.Commands.Auth;
using DanaCopilot.Domain.DTOs;
using DanaCopilot.Domain.Interfaces.Command;
using MediatR;

namespace DanaCopilot.Application.Handlers.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        //private readonly IUserCommandRepository _userCommandRepository;
        //private readonly IMapper _mapper;
        //private readonly ICapPublisher _capBus;
        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository)
        {
            _otpRedisRepository = otpRedisRepository;
           // _userCommandRepository = userCommandRepository;
            //_mapper = mapper;
            //_capBus = capBus;

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
            //Send pm to Notif Service
            //await _capBus.PublishAsync<AuthCommand>("otpevent", new AuthCommand
            //{
            //    MobileNo = request.MobileNo
            //});
            await _otpRedisRepository.Insert(new Otp { UserId = 1, OtpCode = "3265", IsUse = false });
           // TODO main logic
            return true;
        }
    }
}
