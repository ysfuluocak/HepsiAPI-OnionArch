using HepsiAPI.Application.Features.Auths.Rules;
using HepsiAPI.Application.Services.TokenHelper;
using HepsiAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HepsiAPI.Application.Features.Auths.Commands.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthsRules _authRules;

        public LoginCommandHandler(UserManager<AppUser> userManager, ITokenHelper tokenHelper, AuthsRules authRules)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _authRules = authRules;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            await _authRules.EnsureUserExistsAsync(user);

            await _authRules.PasswordShouldBeCorrectAsync(user, request.Password);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenHelper.CreateToken(user, roles);
            var refreshToken = _tokenHelper.CreateRefreshToken();


            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiration = refreshToken.Expiration;
            await _userManager.UpdateAsync(user);

            return new LoginCommandResponse() { AccessToken = token, RefreshToken = refreshToken };
        }
    }

    public class LoginCommandResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
