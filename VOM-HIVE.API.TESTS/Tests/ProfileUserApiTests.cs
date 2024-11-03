using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.TESTS.Data;
using Sp2.Models;
using System.Net.Http.Json;
using VOM_HIVE.API.Models;
using System.Net;

namespace VOM_HIVE.API.TESTS.Tests
{
    [Collection("ApiTests")]
    public class ProfileUserApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ProfileUserApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var token = TokenStorage.Instance.Token;

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não está disponível. Certifique-se de que o teste de login foi executado corretamente.");
            }

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetProfileUser_ReturnListOfProfileUsers()
        {
            // Arrange
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
                nm_user = "Juan",
                pass_user = "q1w2e3r4",
                dt_register = DateTime.Now,
                permission_user = "TODAS",
                status = "MORTO",
                id_company = company.id_company
            };

            _context.Profile_user.Add(profileUser);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync("/api/ProfileUser/ListProfileUsers");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ProfileuserModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetProfileUserByIdProfileUser_ReturnProfileUser()
        {
            // Arrange
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
                nm_user = "Juan",
                pass_user = "q1w2e3r4",
                dt_register = DateTime.Now,
                permission_user = "TODAS",
                status = "MORTO",
                id_company = company.id_company
            };

            _context.Profile_user.Add(profileUser);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/ProfileUser/FindProfilUserById/{profileUser.id_user}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProfileuserModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetProfileUserByIdProfileUser_ReturnNull_WhenPorfileUserDoenstExist()
        {
            // Arrange
            int id_user = 6699;

            // Act
            var response = await _client.GetAsync($"/api/ProfileUser/FindProfilUserById/{id_user}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProfileuserModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task GetProfileUserByIdCompany_ReturnProfileUser()
        {
            // Arrange
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
                nm_user = "Juan",
                pass_user = "q1w2e3r4",
                dt_register = DateTime.Now,
                permission_user = "TODAS",
                status = "MORTO",
                id_company = company.id_company
            };

            _context.Profile_user.Add(profileUser);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/ProfileUser/FindProfileUserByIdCompany/{company.id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            // Tirar o List do service, transformar só em uma instância única
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ProfileuserModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetProfileUserByIdCompany_ReturnNull_WhenCompanyDoenstExist()
        {
            // Arrange
            int id_company = 6699;

            // Act
            var response = await _client.GetAsync($"/api/ProfileUser/FindProfilUserById/{id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProfileuserModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateProfileUser_ReturnsOKAndProfileUser()
        {
            //Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
            };

            _context.Company.Add(company);
            _context.SaveChanges();

            var profileUser = new ProfileuserModel
            {
                nm_user = "Gabriel",
                pass_user = "123456",
                dt_register = DateTime.Now.Date,
                permission_user = "Permission",
                status = "Ativo",
                Company = company
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/ProfileUser/CreateProfileUser", profileUser);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonProfileUser = await response.Content.ReadFromJsonAsync<ResponseModel<ProfileuserModel>>();

            Assert.NotNull(jsonProfileUser.Dados);
            Assert.Equal(profileUser.nm_user, jsonProfileUser.Dados.nm_user);
            Assert.Equal(profileUser.pass_user, jsonProfileUser.Dados.pass_user);
            Assert.Equal(profileUser.dt_register.ToString("yyyy-MM-dd"), jsonProfileUser.Dados.dt_register.ToString("yyyy-MM-dd"));
            Assert.Equal(profileUser.permission_user, jsonProfileUser.Dados.permission_user);
            Assert.Equal(profileUser.status, jsonProfileUser.Dados.status);
        }

        [Fact]
        public async Task CreateprofileUser_ReturnBadRequest_WhenDoesntExistCompany()
        {
            // Arrange
            var profileUser = new ProfileuserModel
            {
                nm_user = "Gabriel",
                pass_user = "123456",
                dt_register = DateTime.Now.Date,
                permission_user = "Permission",
                status = "Ativo",
                //Company = company
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/ProfileUser/CreateProfileUser", profileUser);

            // Asserts
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task EditProfileUser_ReturnsNoContent_WhenProfileUserExist()
        {
            //Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
            };

            _context.Company.Add(company);
            _context.SaveChanges();

            var profileUser = new ProfileuserModel
            {
                nm_user = "Gabriel",
                pass_user = "123456",
                dt_register = DateTime.Now.Date,
                permission_user = "Permission",
                status = "Ativo",
                Company = company
            };

            _context.Profile_user.Add(profileUser);
            _context.SaveChanges();

            var editedProfileUser = new ProfileuserModel
            {
                id_user = profileUser.id_user,
                nm_user = "Juan",
                pass_user = "45678",
                dt_register = DateTime.Now.Date,
                permission_user = "Permission",
                status = "Inativo",
                Company = company
            };

            //Act
            var response = await _client.PutAsJsonAsync($"/api/ProfileUser/EditProfileUser/{profileUser.id_user}", editedProfileUser);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
