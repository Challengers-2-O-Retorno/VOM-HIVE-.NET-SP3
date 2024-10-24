using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using VOM_HIVE.API.Models;
using System.Net.Http.Json;
using Xunit;
using Sp2.Models;
using NuGet.ContentModel;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using VOM_HIVE.API.DTO.Campaign;


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
    }
}