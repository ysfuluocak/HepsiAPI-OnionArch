using HepsiAPI.Domain.Entities;
using System.Security.Claims;

namespace HepsiAPI.Application.Services.TokenHelper
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AppUser user, IList<string> roles);

        RefreshToken CreateRefreshToken();

        //ClaimsPrincipal GetPrincipalFromExpiredToken(string accesToken);
    }
}
