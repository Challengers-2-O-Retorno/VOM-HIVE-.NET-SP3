using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Configuration;
using VOM_HIVE.API.TESTS.Data;
using Xunit.Priority;

namespace VOM_HIVE.API.TESTS.Tests
{
    [Collection("ApiTests")]
    public class CampaignApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public CampaignApiTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task GetCampaigns_ReturnListOfCampaigns()
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
            var response = await _client.GetAsync("/api/Campaign/ListCamapaigns");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.NotEmpty(json.Dados);
            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdCampaign_ReturnCampaign()
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
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignById/{campaign.id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CampaignModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdCampaign_ReturnNullWhenDoenstExist()
        {
            // Arrange
            int id_campaign = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignById/{id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CampaignModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdCompany_ReturnCampaign()
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
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignByIdCompany/{company.id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdCompany_ReturnEmptyList_WhenCompanyDoestExist()
        {
            // Arrange
            int id_company = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignByIdCompany/{id_company}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.Empty(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdProduct_ReturnCampaign()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
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
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
                id_product = product.id_product,
                id_company = company.id_company
            };

            _context.Campaign.Add(campaign);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignByIdProduct/{product.id_product}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdProduct_ReturnEmptyArray_WhenProductDoenstExist()
        {
            // Arrange
            int id_product = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignByIdProduct/{id_product}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.Empty(json.Dados);
        }

        [Fact]
        public async Task CreateCampaign_ReturnOKAndCampaign()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
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
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
                id_product = product.id_product,
                id_company = company.id_company
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Campaign/CreateCampaign", campaign);

            // Asserts
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CampaignModel>>();

            Assert.Equal(campaign.nm_campaign, json.Dados.nm_campaign);
            Assert.Equal(campaign.target, json.Dados.target);
            Assert.Equal(campaign.dt_register.ToString("yyyy-MM-dd"), json.Dados.dt_register.ToString("yyyy-MM-dd"));
            Assert.Equal(campaign.details, json.Dados.details);
            Assert.Equal(campaign.status, json.Dados.status);
            Assert.Equal(campaign.id_product, json.Dados.id_product);
            Assert.Equal(campaign.id_company, json.Dados.id_company);

            Assert.Equal(product.id_product, json.Dados.Product.id_product);
            Assert.Equal(product.nm_product, json.Dados.Product.nm_product);
            Assert.Equal(product.category_product, json.Dados.Product.category_product);

            Assert.Equal(company.id_company, json.Dados.Company.id_company);
            Assert.Equal(company.nm_company, json.Dados.Company.nm_company);
            Assert.Equal(company.cnpj, json.Dados.Company.cnpj);
            Assert.Equal(company.email, json.Dados.Company.email);
            Assert.Equal(company.dt_register.ToString("yyyy-MM-dd"), json.Dados.Company.dt_register.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public async Task CreateCampaign_ReturnNull_WhenDoenstExistCompany()
        {
            // Arrange

            var product = new ProductModel
            {
                nm_product = "BotaCola",
                category_product = "Bebida"
            };

            _context.Product.Add(product);
            _context.SaveChanges();

            var campaign = new CampaignModel
            {
                nm_campaign = "Cola ni mim, cola em nóis",
                target = "adultos",
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
                id_product = product.id_product,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Campaign/CreateCampaign", campaign);

            // Asserts
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CampaignModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateCampaign_ReturnNull_WhenDoesntExistProduct()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
            };

            _context.Company.Add(company);
            _context.SaveChanges();

            var campaign = new CampaignModel
            {
                nm_campaign = "Cola ni mim, cola em nóis",
                target = "adultos",
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
                id_company = company.id_company
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Campaign/CreateCampaign", campaign);

            // Asserts
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadFromJsonAsync<ResponseModel<CampaignModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateCampaign_ReturnBadRequest_WhenNotEnoughtData()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
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
                dt_register = DateTime.Now.Date,
                status = "ativo",
                id_product = product.id_product,
                id_company = company.id_company
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Campaign/CreateCampaign", campaign);

            // Asserts
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task EditCampaign_ReturnsNoContent_WhenCampaignExist()
        {
            // Arrange
            var company = new CompanyModel
            {
                nm_company = "BotaCola Inc.",
                cnpj = "12345678910",
                email = "jaun@company.com.br",
                dt_register = DateTime.Now.Date
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
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
                id_product = product.id_product,
                id_company = company.id_company
            };

            _context.Campaign.Add(campaign);
            _context.SaveChanges();

            var editedCampaign = new CampaignModel
            {
                id_campaign = campaign.id_campaign,
                nm_campaign = "Arroz, feijão, batata o que falta?",
                target = "adulto",
                dt_register = DateTime.Now.Date,
                details = "adulto",
                status = "adulto",
                id_product = product.id_product,
                id_company = company.id_company
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/Campaign/EditCampaign/{campaign.id_campaign}", editedCampaign);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task EditCampaign_ReturnsNotFound_WhenCampaignDoenstExist()
        {
            // Arrange
            int id_campaign = 85287890;

            var editedCampaign = new CampaignModel
            {
                id_campaign = id_campaign,
                nm_campaign = "Arroz, feijão, batata o que falta?",
                target = "adulto",
                dt_register = DateTime.Now.Date,
                details = "adulto",
                status = "adulto",
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/Campaign/EditCampaign/{id_campaign}", editedCampaign);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCampaign_ReturnsNoContent_WhenCampaignExist()
        {
            // Arrange
            var campaign = new CampaignModel
            {
                nm_campaign = "Cola ni mim, cola em nóis",
                target = "adultos",
                dt_register = DateTime.Now.Date,
                details = "alimentos",
                status = "ativo",
            };

            _context.Campaign.Add(campaign);
            _context.SaveChanges();

            // Act
            var response = await _client.DeleteAsync($"/api/Campaign/DeleteCampaign/{campaign.id_campaign}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCampaign_ReturnsNotFound_WhenCampaignDoenstExist()
        {
            // Arrange
            int id_campaign = 6699;

            // Act
            var response = await _client.DeleteAsync($"/api/Campaign/DeleteCampaign/{id_campaign}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
