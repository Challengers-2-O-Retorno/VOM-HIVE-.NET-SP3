using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.DTO.Company;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Company;

namespace VOM_HIVE.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyInterface _companyInterface;

        public CompanyController(ICompanyInterface companyInterface)
        {
            _companyInterface = companyInterface;
        }

        [HttpGet("ListCompanies")]
        [EndpointDescription("Endpoint responsável por listar todas as empresas cadastradas.")]
        public async Task<ActionResult<ResponseModel<List<CompanyModel>>>> ListCompanies()
        {
            var companies = await _companyInterface.ListCompanies();
            return Ok(companies);
        }

        [HttpGet("FindCompanyById/{id_company}")]
        [EndpointDescription("Endpoint responsável por listar uma empresa específica de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyById(int id_company)
        {
            var company = await _companyInterface.FindCompanyById(id_company);
            return Ok(company);
        }

        [HttpGet("FindCompanyByIdCampaign/{id_campaign}")]
        [EndpointDescription("Endpoint responsável por listar uma empresa específica de acordo com o Id de campanha.")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyByIdCampaign(int id_campaign)
        {
            var campaign = await _companyInterface.FindCompanyByIdCampaign(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCompanyByIdProfileUser/{id_user}")]
        [EndpointDescription("Endpoint responsável por listar uma empresa específica de acordo com o Id de usuário.")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyByIdProfileUser(int id_user)
        {
            var user = await _companyInterface.FindCompanyByIdProfileUser(id_user);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("CreateCompany")]
        [EndpointDescription("Endpoint responsável por criar uma nova empresa.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            var company = await _companyInterface.CreateCompany(companyCreateDto);
            return Ok(company);
        }

        [HttpPut("EditCompany/{id_company}")]
        [EndpointDescription("Endpoint responsável por editar uma empresa de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditCompany(int id_company, [FromBody] CompanyEditDto companyEditDto)
        {
            if (id_company != companyEditDto.id_company)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var company = await _companyInterface.EditCompany(companyEditDto);

            if (company.Dados == null) 
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeleteCompany/{id_company}")]
        [EndpointDescription("Endpoint responsável por deletar uma empresa de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditCompany(int id_company)
        {
            var company = await _companyInterface.DeleteCompany(id_company);

            if(company.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
