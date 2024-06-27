using HepsiAPI.Application.Services.TokenHelper;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Infrastructure.Tokens.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace HepsiAPI.Infrastructure.Tokens.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(IOptions<TokenOptions> tokenOptions, IHttpContextAccessor httpContextAccessor)
        {
            _tokenOptions = tokenOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public RefreshToken CreateRefreshToken()
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

            return new RefreshToken
            {
                Token = refreshToken,
                Expiration = refreshTokenExpiration
            };
        }


        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        //{

        //    var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = true,
        //        ValidateIssuer = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidateLifetime = false,
        //        IssuerSigningKey = securityKey,
        //        ValidIssuer = _tokenOptions.Issuer,
        //        ValidAudience = _tokenOptions.Audience,
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
        //    //var jwtSecurityToken = (JwtSecurityToken)securityToken;
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;

        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("Invalid token");

        //    return principal;
        //}



        public AccessToken CreateToken(AppUser user, IList<string> roles)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentilas(securityKey);
            DateTime accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            JwtSecurityToken jwt = CreateJwtSecurityToken(user, roles, accessTokenExpiration, _tokenOptions, signingCredentials);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string? token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(AppUser user, IEnumerable<string> roles, DateTime accessTokenExpiration, TokenOptions tokenOptions, SigningCredentials signingCredentials)
        {
            JwtSecurityToken jwt = new(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, roles),
            signingCredentials: signingCredentials);

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(AppUser user, IEnumerable<string> roles)
        {
            List<Claim> claims = new();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }


    }
}
