using HepsiAPI.Application.Services.Tokens;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Infrastructure.Tokens.Encryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace HepsiAPI.Infrastructure.Tokens.JWT
{
    public class JwtManager : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenOptions _tokenOptions;

        public JwtManager(IOptions<TokenOptions> tokenOptions, UserManager<AppUser> userManager)
        {
            _tokenOptions = tokenOptions.Value;
            _userManager = userManager;
        }

        public string CreateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<AccessToken> CreateToken(AppUser user, IList<string> roles)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentilas(securityKey);
            DateTime accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            JwtSecurityToken jwt = await CreateJwtSecurityToken(user, roles, accessTokenExpiration, _tokenOptions, signingCredentials);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string? token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        private async Task<JwtSecurityToken> CreateJwtSecurityToken(AppUser user, IEnumerable<string> roles, DateTime accessTokenExpiration, TokenOptions tokenOptions, SigningCredentials signingCredentials)
        {
            JwtSecurityToken jwt = new(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, roles),
            signingCredentials: signingCredentials);


            await _userManager.AddClaimsAsync(user, jwt.Claims);

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(AppUser user, IEnumerable<string> roles)
        {
            List<Claim> claims = new();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
