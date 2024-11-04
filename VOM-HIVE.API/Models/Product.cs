using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sp2.Models
{
    [Table("Product")]
    public class ProductModel
    {
        [Key]
        [Column("id_product")]
        public int id_product { get; set; }

        [Required]
        [Column("nm_product")]
        public string? nm_product { get; set; }

        [Required]
        [Column("category_product")]
        public string? category_product { get; set; }

        [JsonIgnore]
        [InverseProperty("Product")]
        public ICollection<CampaignModel> Campaigns { get; set; }
    }
}
