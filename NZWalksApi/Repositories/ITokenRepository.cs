using Microsoft.AspNetCore.Identity;

namespace NZWalksApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWToken(IdentityUser user,List<string> roles);
    }
}
