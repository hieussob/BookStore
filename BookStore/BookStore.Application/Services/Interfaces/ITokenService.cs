using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Application.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(IdentityUser user, IList<string> roles);
    }
}
