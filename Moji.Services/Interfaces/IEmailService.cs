using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendVerificationEmailAsync(string email, string username, string verificationCode);
        Task<bool> SendWelcomeEmailAsync(string email, string username);
        Task<bool> SendVerificationCodeEmailAsync(string email, string verificationCode);

    }
}
