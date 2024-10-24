using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using VOM_HIVE.API.Models;
using System.Net.Http.Json;
using Xunit;
using Sp2.Models;
using NuGet.ContentModel;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using VOM_HIVE.API.DTO.Campaign;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Text;


namespace VOM_HIVE.API.TESTS
{
    public class CampaignApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public CampaignApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCampaings_ReturnsListOfCampaings()
        {
            //Act
            var response = await _client.GetAsync("/api/Campaign/ListCamapaigns");

            //Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<ResponseModel<List<CampaignModel>>>();

            Assert.NotNull(json.Dados);
            Assert.NotEmpty(json.Dados);
        }

        [Fact]
        public async Task PostEndpoint_ReturnsOKStatus()
        {
            var newCampaign = new { nm_campaign = "Test Endpoint", target = "Público Alvo", dt_register = DateTime.Now, details = "Detalhes", status = "Ativo", id_product = 5, id_company = 3 };
            var content = new StringContent(JsonConvert.SerializeObject(newCampaign), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Campaign/CreateCampaign", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PutEndpoint_UpdatesItemSucessfully()
        {
            var attCampaign = new {id_campaign = 47, nm_campaign = "Test Put Endpoint API", target = "Público Alvo", dt_register = DateTime.Now, details = "Detalhes", status = "Ativo", id_product = 5, id_company = 3 };
            var content = new StringContent(JsonConvert.SerializeObject(attCampaign), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/Campaign/EditCampaign/47", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeletEndpoint_RemovesItem()
        {
            var id_campaign = 44;

            var response = await _client.DeleteAsync($"/api/Campaign/DeleteCampaign/{id_campaign}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var delCampaign = await response.Content.ReadAsStringAsync();
            Assert.NotNull( delCampaign );
        }
    }
}