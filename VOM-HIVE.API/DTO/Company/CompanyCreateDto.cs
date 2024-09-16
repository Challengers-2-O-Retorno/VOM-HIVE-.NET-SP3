namespace VOM_HIVE.API.DTO.Company
{
    public class CompanyCreateDto
    {
        public string? nm_company { get; set; }

        public string? cnpj { get; set; }

        public string? email { get; set; }

        public DateTime dt_register { get; set; }
    }
}
