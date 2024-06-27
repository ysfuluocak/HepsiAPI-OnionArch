using HepsiAPI.Application.Exceptions;
using HepsiAPI.Application.Rules;
using HepsiAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HepsiAPI.Application.Features.Auths.Rules
{
    public class AuthsRules : BaseRules
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthsRules(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task CheckIfUserCreatedSuccessfullyAsync(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException("User Not Created");
        }

        public async Task CheckIfAssignRoleToUserAsync(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException("Failed to Assign Role to User");
        }


        public async Task EnsureRoleExistsAsync(string roleName)
        {
            bool isRefreshTokenMissing = string.IsNullOrEmpty(roleName);
            if (isRefreshTokenMissing)
                throw new AuthorizationException("Role Can Not Be Null");

            bool isExists = await _roleManager.RoleExistsAsync(roleName);

            if (!isExists)
                throw new BusinessException($"{roleName} Does Not Exist");
        }

        public async Task UserShouldNotBeExistsAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user is not null)
                throw new AuthorizationException("User Already Exists");
        }

        public Task EnsureUserExistsAsync(AppUser? user)
        {
            if (user is null)
                throw new BusinessException("User Not Found");
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldBeValidAsync(AppUser user, string refreshToken)
        {

            bool isRefreshTokenMissing = string.IsNullOrEmpty(refreshToken);
            if (isRefreshTokenMissing)
                throw new AuthorizationException("Refresh Token Can Not Be Null");

            bool isRefreshTokenInvalid = !user.RefreshToken.Equals(refreshToken);
            if (isRefreshTokenInvalid)
                throw new AuthorizationException("Invalid Refresh Token");

            bool isRefreshTokenExpired = user.RefreshTokenExpiration < DateTime.Now;
            if (isRefreshTokenExpired)
                throw new AuthorizationException("Refresh Token Expired");

            return Task.CompletedTask;
        }

        public async Task PasswordShouldBeCorrectAsync(AppUser? user, string password)
        {
            bool checkPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!checkPassword)
                throw new AuthorizationException("Incorrect Password");
        }
    }
}
