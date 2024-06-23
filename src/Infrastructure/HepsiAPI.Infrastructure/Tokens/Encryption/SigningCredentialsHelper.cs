using Microsoft.IdentityModel.Tokens;

namespace HepsiAPI.Infrastructure.Tokens.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentilas(SecurityKey key)
        {
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
