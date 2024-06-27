using HepsiAPI.Application.Features.Auths.Rules;
using HepsiAPI.Application.Services.TokenHelper;
using HepsiAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HepsiAPI.Application.Features.Auths.Commands.CreateRefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthsRules _authRules;

        public RefreshTokenCommandHandler(ITokenHelper tokenHelper, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, AuthsRules authRules)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _authRules = authRules;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {

            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];


            await _authRules.EnsureUserExistsAsync(user);

            await _authRules.RefreshTokenShouldBeValidAsync(user, refreshToken);


            var roles = await _userManager.GetRolesAsync(user);

            var newToken = _tokenHelper.CreateToken(user, roles);
            var newRefreshToken = _tokenHelper.CreateRefreshToken();

            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpiration = newRefreshToken.Expiration;

            await _userManager.UpdateAsync(user);

            var response = new RefreshTokenCommandResponse()
            {
                AccessToken = newToken,
                RefreshToken = newRefreshToken
            };

            return response;
        }

    }

    public class RefreshTokenCommandResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
