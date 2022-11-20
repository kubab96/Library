using Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
