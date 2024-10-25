using Microsoft.EntityFrameworkCore;
using Sp2.Models;
using System;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Campaign;
using VOM_HIVE.API.DTO.Campaign.Vinculo;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Campaign
{
    public class CampaignService : ICampaignInterface
    {
        private readonly AppDbContext _context;

        public CampaignService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<CampaignModel>>> ListCampaign()
        {
            ResponseModel<List<CampaignModel>> resposta = new ResponseModel<List<CampaignModel>>();
            try
            {
                var campaign = await _context.Campaign.Include(c => c.Company).Include(c => c.Product).ToListAsync();

                resposta.Dados = campaign;
                resposta.Mensagem = "Todas campanhas coletadas!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
        public async Task<ResponseModel<CampaignModel>> FindCampaignById(int id_campaign)
        {
            ResponseModel<CampaignModel> resposta = new ResponseModel<CampaignModel>();
            try
            {
                var campaign = await _context.Campaign.Include(c => c.Company).Include(c => c.Product).FirstOrDefaultAsync(campaignDb => campaignDb.id_campaign == id_campaign);

                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhuma campanha localizada!";
                    return resposta;
                }

                resposta.Dados = campaign;
                resposta.Mensagem = "Camapanha localizada!";
                return resposta;

            } catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<List<CampaignModel>>> FindCampaignByIdCompany(int id_company)
        {
            ResponseModel<List<CampaignModel>> resposta = new ResponseModel<List<CampaignModel>>();
            try
            {
                var campaign = await _context.Campaign
                    .Include(pu => pu.Company)
                    .Where(campaignDb => campaignDb.Company.id_company == id_company)
                    .ToListAsync();

                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhuma campanha localizada!";
                    return resposta;
                }

                resposta.Dados = campaign;
                resposta.Mensagem = "Campanha localizada!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<CampaignModel>>> FindCampaignByIdProduct(int id_product)
        {
            ResponseModel<List<CampaignModel>> resposta = new ResponseModel<List<CampaignModel>>();
            try
            {
                var campaign = await _context.Campaign
                    .Include(pu => pu.Company)
                    .Where(campaignDb => campaignDb.Product.id_product == id_product)
                    .ToListAsync();

                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhuma campanha localizada!";
                    return resposta;
                }

                resposta.Dados = campaign;
                resposta.Mensagem = "Campanha localizada!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }



        public async Task<ResponseModel<CampaignModel>> CreateCampaign(CampaignCreateDto campaignCreateDto)
        {
            ResponseModel<CampaignModel> resposta = new ResponseModel<CampaignModel>();
            try
            {
                var campaignDb = await _context.Campaign
                    .Include(c => c.Company)
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(
                        c =>
                        c.Company.id_company == campaignCreateDto.id_company &&
                        c.Product.id_product == campaignCreateDto.id_product
                    );

                var company = await _context.Company.FirstOrDefaultAsync(companyDb => companyDb.id_company == campaignCreateDto.id_company);

                if (company == null)
                {
                    resposta.Mensagem = "Empresa não encontrada!";
                    return resposta;
                }

                var product = await _context.Product.FirstOrDefaultAsync(productDb => productDb.id_product == campaignCreateDto.id_product);

                if (product == null)
                {
                    resposta.Mensagem = "Produto não encontrado.";
                    return resposta;
                }

                var campaign = new CampaignModel()
                {
                    id_company = campaignCreateDto.id_company,
                    id_product = campaignCreateDto.id_product,
                    nm_campaign = campaignCreateDto.nm_campaign,
                    target = campaignCreateDto.target,
                    dt_register = DateTime.Now,
                    details = campaignCreateDto.details,
                    status = campaignCreateDto.status,
                    Company = company,
                    Product = product
                };

                _context.Add(campaign);
                await _context.SaveChangesAsync();

                resposta.Dados = campaign;

                //resposta.Dados = await _context.Campaign
                //    .Include(c => c.Company)
                //    .Include(c => c.Product)
                //    .ToListAsync();
                resposta.Mensagem = "Campanha criada com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Ocorreu um erro ao criar a campanha: " + ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<CampaignModel>>> DeleteCampaign(int id_campaign)
        {
            ResponseModel<List<CampaignModel>> resposta = new ResponseModel<List<CampaignModel>>();
            try
            {
                var campaign = await _context.Campaign
                    .FirstOrDefaultAsync(campaignDb => campaignDb.id_campaign == id_campaign);

                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhuma campanha encontrada!";
                    return resposta;
                }

                _context.Remove(campaign);
                await _context.SaveChangesAsync();

                resposta.Mensagem = "Campanha removida com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<CampaignModel>> EditCampaign(CampaignEditDto campaignEditDto)
        {
            ResponseModel<CampaignModel> resposta = new ResponseModel<CampaignModel>();
            try
            {
                var campaign = await _context.Campaign
                    .Include(c => c.Company)
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(
                        campaignDb => campaignDb.id_campaign == campaignEditDto.id_campaign
                        //c =>
                        //c.Company.id_company == campaignEditDto.id_company &&
                        //c.Product.id_product == campaignEditDto.id_product
                    );

                //var campaign = await _context.Campaign
                //    .Include(c => c.Company)
                //    .Include(c => c.Product)
                //    .Include(c=> c.Company.Profile_users)
                //    .FirstOrDefaultAsync(campaignDb => campaignDb.id_campaign == campaignEditDto.id_campaign);
                if (campaign == null)
                {
                    resposta.Mensagem = "Nenhum registro de campanha localizado!";
                    return resposta;
                }

                var company = await _context.Company
                    .FirstOrDefaultAsync(companyDb => companyDb.id_company == campaignEditDto.id_company);
                if (company == null)
                {
                    resposta.Mensagem = "Nenhum registro de empresa localizado!";
                    return resposta;
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(productDb => productDb.id_product == campaignEditDto.id_product);
                if (product == null)
                {
                    resposta.Mensagem = "Nenhum registro de produto localizado!";
                    return resposta;
                }

                campaign.nm_campaign = campaignEditDto.nm_campaign;
                campaign.target = campaignEditDto.target;
                campaign.details = campaignEditDto.details;
                campaign.status = campaignEditDto.status;
                campaign.Product = product;
                campaign.Company = company;

                resposta.Mensagem = "Campanha editada com sucesso!";
                resposta.Status = true;

                _context.Update(campaign);
                await _context.SaveChangesAsync();

                resposta.Dados = campaign;
                return resposta;
            }
            catch(DbUpdateException dbEx)
            {
                resposta.Mensagem = "Erro ao atualizar o banco de dados: " + dbEx.Message;
                resposta.Status = false;
                return resposta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
