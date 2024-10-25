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

namespace VOM_HIVE.API.TESTS.Tests
{
    public class ProfileUserApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ProfileUserApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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
    }
}
