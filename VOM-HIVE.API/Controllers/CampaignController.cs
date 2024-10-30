using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Campaign;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Campaign;
using VOM_HIVE.API.Services.ProfileUser;

namespace VOM_HIVE.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignInterface _campaignInterface;
        private readonly AppDbContext _context;

        public CampaignController(ICampaignInterface campaignInterface, AppDbContext context)
        {
            _campaignInterface = campaignInterface;
            _context = context;
        }

        [HttpGet("ListCamapaigns")]
        [EndpointDescription("Endpoint responsável por listar todas as campanhas cadastradas.")]
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> ListCamapaigns()
        {
            var campaigns = await _campaignInterface.ListCampaign();
            return Ok(campaigns);
        }

        [HttpGet("FindCampaignById/{id_campaign}")]
        [EndpointDescription("Endpoint responsável para listar uma campanha específica de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> ListCamapaigns(int id_campaign)
        {
            var campaign = await _campaignInterface.FindCampaignById(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCampaignByIdCompany/{id_campaign}")]
        [EndpointDescription("Endpoint responsável para listar uma campanha específica de acordo com o Id de empresa.")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> FindProfileUserByIdCompany(int id_campaign)
        {
            var campaign = await _campaignInterface.FindCampaignByIdCompany(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCampaignByIdProduct/{id_product}")]
        [EndpointDescription("Endpoint responsável para listar uma campanha específica de acordo com o Id de produto.")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> FindCampaignByIdProduct(int id_product)
        {
            var campaign = await _campaignInterface.FindCampaignByIdProduct(id_product);
            return Ok(campaign);
        }

        [AllowAnonymous]
        [HttpPost("CreateCampaign")]
        [EndpointDescription("Endpoint responsável para criação de uma nova campanha.")]
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> CreateCampaign(CampaignCreateDto campaignCreateDto)
        {
            var campaign = await _campaignInterface.CreateCampaign(campaignCreateDto);
            return Ok(campaign);
        }

        [HttpPut("EditCampaign/{id_campaign}")]
        [EndpointDescription("Endpoint responsável para editar uma campanha de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> EditCampaign(int id_campaign, [FromBody] CampaignEditDto campaignEditDto)
        {
            if (id_campaign != campaignEditDto.id_campaign)
            {
                return BadRequest("ID na URL e no corpo não coincidem");
            }

            var campaign = await _campaignInterface.EditCampaign(campaignEditDto);

            if (campaign.Dados == null)
            {
                return NotFound("Campanha não encontrada");
            }

            return NoContent();
        }

        [HttpDelete("DeleteCampaign/{id_campaign}")]
        [EndpointDescription("Endpoint responsável por deletar uma campanha de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> DeleteCampaign(int id_campaign)
        {
            var campaign = await _campaignInterface.DeleteCampaign(id_campaign);

            if (campaign.Dados == null)
            {
                return NotFound();
            }

            //var campaign = await _campaignInterface.DeleteCampaign(id_campaign);
            return NoContent();
        }
    }
}
