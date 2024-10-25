using Sp2.Models;
using VOM_HIVE.API.DTO.Company;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Company
{
    public interface ICompanyInterface
    {
        Task<ResponseModel<List<CompanyModel>>> ListCompanies();
        Task<ResponseModel<CompanyModel>> FindCompanyById(int id_company);
        Task<ResponseModel<CompanyModel>> FindCompanyByIdCampaign(int id_campaign);
        Task<ResponseModel<CompanyModel>> FindCompanyByIdProfileUser(int id_user);
        Task<ResponseModel<CompanyModel>> CreateCompany(CompanyCreateDto CompanyCreateDto);
        Task<ResponseModel<CompanyModel>> EditCompany(CompanyEditDto CompanyEditDto);
        Task<ResponseModel<CompanyModel>> DeleteCompany(int id_company);
    }
}
