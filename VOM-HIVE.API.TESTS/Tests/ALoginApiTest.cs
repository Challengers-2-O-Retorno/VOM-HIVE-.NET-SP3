using Microsoft.Extensions.DependencyInjection;
using Sp2.Models;
using System.Net;
using System.Net.Http.Json;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.TESTS.Data;

namespace VOM_HIVE.API.TESTS.Tests
{
    [Collection("ApiTests")]
    public class ALoginApiTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ALoginApiTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        private string _token;

        [Fact]
        public async Task ALogin_ReturnsToken_WhenCredentialsAreValid()
        {
            //Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now
            };

            _context.Company.Add(company);
            _context.SaveChanges();

            var profileUser = new ProfileuserModel
            {
                nm_user = "teste",
                pass_user = "password",
                dt_register = DateTime.Now,
                permission_user = "TODAS",
                status = "MORTO",
                id_company = company.id_company
            };

            _context.Profile_user.Add(profileUser);
            _context.SaveChanges();

            var login = new LoginModel
            {
                nm_user = profileUser.nm_user,
                pass_user = profileUser.pass_user,
            };

            //Act
            var response = await _client.PostAsJsonAsync($"/api/Login/Login", login);

            //Assert
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadFromJsonAsync<TokenModel>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(token);

            Assert.NotNull(token.token);

            TokenStorage.Instance.Token = token.token;
        }
    }

    public class TokenModel
    {
        public string token { get; set; }
    }

    public class TokenStorage
    {
        public static TokenStorage _instance;
        public static readonly object _lock = new object();

        public static TokenStorage Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new TokenStorage();
                }
            }
        }
        public string Token { get; set; }
    }
}
