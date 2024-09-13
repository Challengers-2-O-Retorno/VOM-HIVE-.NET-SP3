using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sp2.Models
{
    [Table("Company")]
    public class CompanyModel
    {
        [Key]
        [Column("id_company")]
        public int id_company { get; set; }

        [Required]
        [Column("nm_company")]
        public string? nm_company { get; set; }

        [Required]
        [Column("cnpj")]
        public string? cnpj { get; set; }

        [Required]
        [Column("email")]
        public string? email { get; set; }

        [Required]
        [Column("dt_register")]
        public DateTime dt_register { get; set; }
        
        [JsonIgnore]
        [InverseProperty("Company")]
        public ICollection<CampaignModel> Campaigns { get; set; }

        [JsonIgnore]
        [InverseProperty("Company")]
        public ICollection<ProfileuserModel> Profile_users { get; set; }
    }
}
