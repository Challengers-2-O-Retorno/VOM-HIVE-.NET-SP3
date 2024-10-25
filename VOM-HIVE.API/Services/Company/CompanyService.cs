using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sp2.Models;
using System.Linq.Expressions;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Company;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Company
{
    public class CompanyService : ICompanyInterface
    {
        private readonly AppDbContext _context;
        public CompanyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<CompanyModel>>> ListCompanies()
        {
            ResponseModel<List<CompanyModel>> resposta = new ResponseModel<List<CompanyModel>>();
            try
            {
                var companies = await _context.Company.ToListAsync();

                resposta.Dados = companies;
                resposta.Mensagem = "Todas as empresas foram coletadas!";
                return resposta;

            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> FindCompanyById(int id_company)
        {
            ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();

            try
            {
                var company = await _context.Company.FirstOrDefaultAsync(companyDb => companyDb.id_company == id_company);

                if (company == null)
                {
                    resposta.Mensagem = "Nenhuma empresa localizada!";
                    return resposta;
                }

                resposta.Dados = company;
                resposta.Mensagem = "Empresa localizada!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> FindCompanyByIdCampaign(int id_campaign)
        {
            ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();

            try
            {
                var  campaign = await _context.Campaign
                    .Include(co => co.Company)
                    .FirstOrDefaultAsync(companyDb => companyDb.id_campaign == id_campaign);

                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhuma empresa localizada!";
                    return resposta;
                }

                resposta.Dados = campaign.Company;
                resposta.Mensagem = "Empresa localizada!";
                return resposta;

            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> FindCompanyByIdProfileUser(int id_user)
        {
            ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();

            try
            {
                var user = await _context.Profile_user
                    .Include(co => co.Company)
                    .FirstOrDefaultAsync(companyDb => companyDb.id_user == id_user);

                if (user == null)
                {
                    resposta.Mensagem = "Nenhuma empresa localizada!";
                    return resposta;
                }

                resposta.Dados = user.Company;
                resposta.Mensagem = "Empresa localizada!";
                return resposta;

            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> CreateCompany(CompanyCreateDto CompanyCreateDto)
        {
            ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();

            try
            {
                var company = new CompanyModel()
                {
                    nm_company = CompanyCreateDto.nm_company,
                    cnpj = CompanyCreateDto.cnpj,
                    email = CompanyCreateDto.email,
                    dt_register = DateTime.Now
                };

                _context.Add(company);
                await _context.SaveChangesAsync();

                resposta.Dados = company;
                resposta.Mensagem = "Empresa criada com sucesso!";
                return resposta;
                
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> EditCompany(CompanyEditDto CompanyEditDto)
        {
        ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();
            try
            {
                var company = await _context.Company
                    .FirstOrDefaultAsync(
                    comapnyDb => comapnyDb.id_company == CompanyEditDto.id_company
                    );

                if ( company == null)
                {
                    resposta.Mensagem = "Nenhuma empresa localizada!";
                    return resposta;
                }

                company.nm_company = CompanyEditDto.nm_company;
                company.cnpj = CompanyEditDto.cnpj;
                company.email = CompanyEditDto.email;

                _context.Update(company);
                await _context.SaveChangesAsync();
                
                resposta.Dados = company;
                resposta.Mensagem = "Empresa editada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CompanyModel>> DeleteCompany(int id_company)
        {
            ResponseModel<CompanyModel> resposta = new ResponseModel<CompanyModel>();
            try
            {
                var company = await _context.Company.FirstOrDefaultAsync(companyDb => companyDb.id_company == id_company);

                if (company == null)
                {
                    resposta.Mensagem = "Nenhuma empresa localizada!";
                    return resposta;
                }

                _context.Remove(company);
                await _context.SaveChangesAsync();

                resposta.Dados = company;
                resposta.Mensagem = "Empresa removida com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
