using BookStore.Application.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> RegisterUser(LoginUser user, string role);
        public Task<int> Login(LoginUser user);
        public Task<string> GenerateTokenStringAsync(string email);
        public Task<IdentityUser> GetCurrentUserAsync(string email);
        public Task<bool> DeleteUserAndRolesAsync(string email);
        public Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword);
    }
}
