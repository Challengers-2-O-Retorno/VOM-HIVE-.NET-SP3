using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using VOM_HIVE.API.Auth;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Configuration;

namespace VOM_HIVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IAuthenticateInterface _authenticateInterface;
        public readonly IConfigurationInterface _configurationInterface;

        public LoginController(IAuthenticateInterface authenticateInterface, AppDbContext context, IConfigurationInterface configurationInterface)
        {
            _authenticateInterface = authenticateInterface;
            _context = context;
            _configurationInterface = configurationInterface;
        }

        [HttpPost("Login")]
        [EndpointDescription("Endpoint para o usuário logar e poder obter o Token de autenticação.")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var exist = await _authenticateInterface.userExists(model.nm_user);
            if (!exist)
            {
                return Unauthorized("Usuário não existe.");
            }

            var result = await _authenticateInterface.AuthenticateAsync(model.nm_user, model.pass_user);
            if (!result)
            {
                return Unauthorized("Usuário ou senha inválido.");
            }

            var token = await GetTokenFromAuth0();
            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao gerar o token de autenticação");
            }

            return Ok(new { Token = token });
        }

        private async Task<bool> ValidateUser(string nm_user, string pass_user)
        {
            var user = await _context.Profile_user
                .Where(x => x.nm_user == nm_user && x.pass_user == pass_user)
                .FirstOrDefaultAsync();
            return user != null;
        }

        private async Task<string> GetTokenFromAuth0()
        {
            var client = new RestClient($"{_configurationInterface.Authority}oauth/token");
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", $"{{\"client_id\":\"{_configurationInterface.ClientId}\",\"client_secret\":\"{_configurationInterface.ClientSecret}\",\"audience\":\"{_configurationInterface.Audience}\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var tokenReponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                return tokenReponse?.access_token;
            }
            return null;
        }

        public class TokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
        }
    }
}
