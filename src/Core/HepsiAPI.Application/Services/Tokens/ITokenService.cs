using HepsiAPI.Domain.Entities;

namespace HepsiAPI.Application.Services.Tokens
{
    public interface ITokenService
    {
        Task<AccessToken> CreateToken(AppUser user, IList<string> roles);

        string CreateRefreshToken();
    }
}
