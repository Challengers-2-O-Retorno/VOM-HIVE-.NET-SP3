using Microsoft.EntityFrameworkCore;
using VOM_HIVE.API.Auth;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Services.Configuration;

namespace VOM_HIVE.API.Services.Authenticate
{
    public class AuthenticateService : IAuthenticateInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationInterface _configuration;

        public AuthenticateService(AppDbContext context, IConfigurationInterface configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string nm_user, string senha)
        {
            var user = await _context.Profile_user
                .FirstOrDefaultAsync(u => u.nm_user == nm_user && u.pass_user == senha);
            //var user = await _context.Profile_user.Where(x => x.nm_user == nm_user).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> userExists(string nm_user)
        {
            var user = await _context.Profile_user.Where(x => x.nm_user == nm_user).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
