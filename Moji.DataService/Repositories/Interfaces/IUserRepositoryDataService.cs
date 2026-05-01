using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Repositories.Interfaces
{
    public interface IUserRepositoryDataService
    {
        Task<RegisterLoginResponse> RegisterAndLoginAsync(RegisterLoginRequest request,
      string passwordHash, string refreshToken, string verificationToken);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckUsernameExistsAsync(string username);
    }
}
