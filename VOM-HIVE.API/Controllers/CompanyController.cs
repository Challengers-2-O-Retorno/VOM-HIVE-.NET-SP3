using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.DTO.Company;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Company;

namespace VOM_HIVE.API.Controllers
{
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
        public async Task<ActionResult<ResponseModel<List<CompanyModel>>>> ListCompanies()
        {
            var companies = await _companyInterface.ListCompanies();
            return Ok(companies);
        }

        [HttpGet("FindCompanyById/{id_company}")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyById(int id_company)
        {
            var company = await _companyInterface.FindCompanyById(id_company);
            return Ok(company);
        }

        [HttpGet("FindCompanyByIdCampaign/{id_campaign}")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyByIdCampaign(int id_campaign)
        {
            var campaign = await _companyInterface.FindCompanyByIdCampaign(id_campaign);
            return Ok(campaign);
        }

        [HttpGet("FindCompanyByIdProfileUser/{id_user}")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindCompanyByIdProfileUser(int id_user)
        {
            var user = await _companyInterface.FindCompanyByIdProfileUser(id_user);
            return Ok(user);
        }

        [HttpPost("CreateCompany")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            var company = await _companyInterface.CreateCompany(companyCreateDto);
            return Ok(company);
        }

        [HttpPut("EditCompany")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditCompany(CompanyEditDto companyEditDto)
        {
            var company = await _companyInterface.EditCompany(companyEditDto);
            return Ok(company);
        }

        [HttpDelete("DeleteCompany")]
        public async Task<ActionResult<ResponseModel<ProductModel>>> EditCompany(int id_company)
        {
            var company = await _companyInterface.DeleteCompany(id_company);
            return Ok(company);
        }
    }
}
