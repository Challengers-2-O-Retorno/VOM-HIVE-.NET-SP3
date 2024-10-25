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
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> ListCamapaigns()
        {
            var campaigns = await _campaignInterface.ListCampaign();
            return Ok(campaigns);
        }

        [HttpGet("FindCampaignById/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> ListCamapaigns(int id_campaign)
        {
            var campaign = await _campaignInterface.FindCampaignById(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCampaignByIdCompany/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> FindProfileUserByIdCompany(int id_campaign)
        {
            var campaign = await _campaignInterface.FindCampaignByIdCompany(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCampaignByIdProduct/{id_product}")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> FindCampaignByIdProduct(int id_product)
        {
            var campaign = await _campaignInterface.FindCampaignByIdProduct(id_product);
            return Ok(campaign);
        }

        [HttpPost("CreateCampaign")]
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> CreateCampaign(CampaignCreateDto campaignCreateDto)
        {
            var campaign = await _campaignInterface.CreateCampaign(campaignCreateDto);
            return Ok(campaign);
        }

        [HttpPut("EditCampaign/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<CampaignModel>>> EditCampaign(int id_campaign, [FromBody] CampaignEditDto campaignEditDto)
        {
            if (id_campaign != campaignEditDto.id_campaign)
            {
                return BadRequest("ID na URL e no corpo não coincidem");
            }

            var campaign = await _campaignInterface.EditCampaign(campaignEditDto);

            if (campaign == null)
            {
                return NotFound();
            }
            //var campaign = await _context.Campaign.Add(campaignEditDto);

            //_context.Entry(campaignEditDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return Ok(campaignEditDto);

            //var campaign = await _campaignInterface.EditCampaign(campaignEditDto);
            return Ok(campaign);
        }

        [HttpDelete("DeleteCampaign/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<List<CampaignModel>>>> DeleteCampaign(int id_campaign)
        {
            var campaign = await _campaignInterface.DeleteCampaign(id_campaign);

            if (campaign == null)
            {
                return NotFound();
            }

            //var campaign = await _campaignInterface.DeleteCampaign(id_campaign);
            return Ok(campaign);
        }
    }
}
