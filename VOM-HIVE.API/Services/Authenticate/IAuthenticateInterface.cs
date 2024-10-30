using Sp2.Models;

namespace VOM_HIVE.API.Auth
{
    public interface IAuthenticateInterface
    {
        Task<bool> AuthenticateAsync(string nm_user, string senha);
        Task<bool> userExists(string nm_user);
    }
}
