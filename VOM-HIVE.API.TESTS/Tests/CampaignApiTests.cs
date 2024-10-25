using Microsoft.Extensions.DependencyInjection;
using Sp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.TESTS.Data;

namespace VOM_HIVE.API.TESTS.Tests
{
    public class CampaignApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public CampaignApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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
            var response = await _client.GetAsync($"/api/Campaign/FindCampaignByIdProduct/{product.id_product}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetCampaignsByIdProduct_ReturnEmptyArray_WhenDoenstExist()
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
    }
}
