using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<String> roles);
    }
}
