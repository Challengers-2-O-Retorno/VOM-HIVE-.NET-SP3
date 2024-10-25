using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Sp2.Models;
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
            _context.Product.Add(new ProductModel { nm_product = "produtoTeste1", category_product = "categoriaProduto1" });
            _context.Product.Add(new ProductModel { nm_product = "produtoTeste2", category_product = "categoriaProduto2" });
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync("/api/Product/ListarProdutos");

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<ProductModel>>>();

            Assert.NotNull(json.Dados);
            Assert.NotEmpty(json.Dados);  // Verifica que a lista de produtos não está vazia
        }
    }
}
