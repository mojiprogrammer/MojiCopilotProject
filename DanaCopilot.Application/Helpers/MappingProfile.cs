using AutoMapper;
using DanaCopilot.Application.Commands.Auth;
using DanaCopilot.Domain.Entities;

namespace DanaCopilot.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthCommand, UserComEntity>().ReverseMap();
        }
    }
}
