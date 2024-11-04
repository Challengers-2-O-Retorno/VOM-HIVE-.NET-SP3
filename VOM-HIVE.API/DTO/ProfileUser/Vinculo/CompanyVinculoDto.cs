namespace VOM_HIVE.API.DTO.ProfileUser.Vinculo
{
    public class CompanyVinculoDto
    {
        public int id_company { get; set; }

        public string? nm_company { get; set; }

        public string? cnpj { get; set; }

        public string? email { get; set; }

        public DateTime dt_register { get; set; }
    }
}
