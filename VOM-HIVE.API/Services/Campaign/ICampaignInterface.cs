﻿using Sp2.Models;
using VOM_HIVE.API.DTO.Campaign;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.Campaign
{
    public interface ICampaignInterface
    {
        Task<ResponseModel<List<CampaignModel>>> ListCampaign();
        Task<ResponseModel<CampaignModel>> FindCampaignById(int id_campaign);
        Task<ResponseModel<List<CampaignModel>>> FindCampaignByIdCompany(int id_company);
        Task<ResponseModel<List<CampaignModel>>> FindCampaignByIdProduct(int id_product);
        Task<ResponseModel<List<CampaignModel>>> CreateCampaign(CampaignCreateDto campaignCreateDto);
        Task<ResponseModel<List<CampaignModel>>> EditCampaign(CampaignEditDto campaignEditDto);
        Task<ResponseModel<List<CampaignModel>>> DeleteCampaign(int id_campaign);
    }
}