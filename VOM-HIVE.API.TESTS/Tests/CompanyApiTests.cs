using Microsoft.Extensions.DependencyInjection;
using Sp2.Models;
using System.Net.Http.Json;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.TESTS.Data;

namespace VOM_HIVE.API.TESTS.Tests
{
    public class CompanyApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public CompanyApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        }

        [Fact]
        public async Task GetCompanys_ReturnListOfCompanys()
        {
            // Arrange
            _context.Company.Add(new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now
            });

            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync("/api/Company/ListCompanies");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CompanyModel>>>();

            Assert.NotNull(json.Dados);
            Assert.NotEmpty(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdCompany_ReturnCompany()
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

            // Act
            var response = await _client.GetAsync($"/api/Company/FindCompanyById/{company.id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdCompany_ReturnNull_WhenCompanyDoenstExist()
        {
            // Arrange
            int id_company = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Company/FindCompanyById/{id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdCampaign_ReturnCompany()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now
            };

            var product = new ProductModel
            {
                nm_product = "BotaCola",
                category_product = "Bebida"
            };

            _context.Company.Add(company);
            _context.Product.Add(product);
            _context.SaveChanges();

            var campaign = new CampaignModel
            {
                nm_campaign = "Cola ni mim, cola em nóis",
                target = "adultos",
                dt_register = DateTime.Now,
                details = "alimentos",
                status = "ativo",
                id_product = product.id_product,
                id_company = company.id_company
            };

            _context.Campaign.Add(campaign);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/Company/FindCompanyByIdCampaign/{campaign.id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdCampaign_ReturnNull_WhenCompanyDoenstExist()
        {
            // Arrange
            int id_campaign = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Company/FindCompanyByIdCampaign/{id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdProfileUser_ReturnCompany()
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
            var response = await _client.GetAsync($"/api/Company/FindCompanyByIdProfileUser/{profileUser.id_user}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCompanyByIdProfileUser_ReturnNull_WhenDoenstExist()
        {
            // Arrange
            int id_user = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Company/FindCompanyByIdProfileUser/{id_user}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CompanyModel>>();

            Assert.Null(json.Dados);
        }
    }
}
