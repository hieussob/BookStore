using BookStore.Application.DTOs;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.IRepositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IUserRepository userRepository, ITokenService tokenService,
                            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterUser(LoginUser user, string role)
        {
            var identityUser = new IdentityUser { UserName = user.UserName, Email = user.UserName };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            if (!result.Succeeded) return false;

            var roleExists = await _roleManager.FindByNameAsync(role);
            if (roleExists == null) return false;

            var roleResult = await _userManager.AddToRoleAsync(identityUser.Id, role);
            return roleResult.Succeeded;
        }

        public async Task<int> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser == null) return 0;

            if (identityUser.LockoutEndDateUtc.HasValue && identityUser.LockoutEndDateUtc > DateTimeOffset.Now) return 2;

            return await _userManager.CheckPasswordAsync(identityUser, user.Password) ? 1 : 0;
        }

        public async Task<string> GenerateTokenStringAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;

            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null) return null;

            var roles = await _userManager.GetRolesAsync(identityUser.Id);

            return _tokenService.GenerateToken(identityUser, roles);
        }

        public async Task<IdentityUser> GetCurrentUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> DeleteUserAndRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(user.Id);

            foreach (var role in roles)
            {
                var roleResult = await _userManager.RemoveFromRoleAsync(user.Id, role);
                if (!roleResult.Succeeded)
                {
                    return false;
                }
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user.Id, currentPassword, newPassword);
        }
    }
}
