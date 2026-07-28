using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Commands.Auth
{
    public class AuthCommand:IRequest<bool>
    {
        public required string MobileNo { get; set; }
    }
}
