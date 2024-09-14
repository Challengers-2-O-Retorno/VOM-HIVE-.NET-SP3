using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VOM_HIVE.API.DTO.Product
{
    public class ProductEditDto
    {
        public int id_product { get; set; }
        public string? nm_product { get; set; }
        public string? category_product { get; set; }
    }
}
