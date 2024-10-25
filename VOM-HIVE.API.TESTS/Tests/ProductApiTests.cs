using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Sp2.Models;
using System.Net;
using System.Net.Http.Json;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Product;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.TESTS.Data;

namespace VOM_HIVE.API.TESTS.Tests
{
    public class ProductApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _context;

        public ProductApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            _context.Product.Add(new ProductModel
            {
                nm_product = "produtoTeste1",
                category_product = "categoriaProduto1"
            });
            _context.Product.Add(new ProductModel
            {
                nm_product = "produtoTeste2",
                category_product = "categoriaProduto2"
            });

            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync("/api/Product/ListarProdutos");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ProductModel>>>();

            Assert.NotNull(json.Dados);
            Assert.NotEmpty(json.Dados);
        }

        [Fact]
        public async Task GetProductByIdProduct_ReturnProduct()
        {
            // Arrange
            int id_product = 1;
            _context.Product.Add(new ProductModel
            {
                nm_product = "produtoTeste1",
                category_product = "categoriaProduto1"
            });
            _context.Product.Add(new ProductModel
            {
                nm_product = "produtoTeste2",
                category_product = "categoriaProduto2"
            });

            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/Product/BuscarProdutoPorId/{id_product}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetProductByIdProduct_ReturnNull_WhenDoenstExist()
        {
            // Arrange
            int id_product = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Product/BuscarProdutoPorId/{id_product}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task GetProductByIdCompaign_ReturnProduct()
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
            var response = await _client.GetAsync($"/api/Product/BuscarProdutoPorIdCampaign/{campaign.id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task GetProductByIdCompaign_ReturnNull_WhenCampaignDoenstExist()
        {
            // Arrange
            int id_campaign = 6699;

            // Act
            var response = await _client.GetAsync($"/api/Product/BuscarProdutoPorIdCampaign/{id_campaign}");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();

            Assert.Null(json.Dados);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProduct()
        {
            // Arrange
            var product = new ProductModel
            {
                nm_product = "BotaCola",
                category_product = "Bebida"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Product/CreateProduct", product);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonProduct = await response.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();


            Assert.Equal(product.nm_product, jsonProduct.Dados.nm_product);
            //Assert.Equal(product.category_product, product.category_product);
        }
    }
}
