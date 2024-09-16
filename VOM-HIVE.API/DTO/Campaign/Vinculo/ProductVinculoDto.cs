using Sp2.Models;

namespace VOM_HIVE.API.DTO.Campaign.Vinculo
{
    public class ProductVinculoDto
    {
        public int id_product { get; set; }
        public string? nm_product { get; set; }
        public string? category_product { get; set; }
        public ICollection<CampaignModel> Campaigns { get; set; }
    }
}
