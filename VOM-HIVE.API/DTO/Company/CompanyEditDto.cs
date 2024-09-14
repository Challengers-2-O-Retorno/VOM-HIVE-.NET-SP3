using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VOM_HIVE.API.DTO.Company
{
    public class CompanyEditDto
    {
        public int id_company { get; set; }

        public string? nm_company { get; set; }

        public string? cnpj { get; set; }

        public string? email { get; set; }
    }
}
