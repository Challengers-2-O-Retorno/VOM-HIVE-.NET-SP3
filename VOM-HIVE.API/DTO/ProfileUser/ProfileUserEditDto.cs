using VOM_HIVE.API.DTO.ProfileUser.Vinculo;

namespace VOM_HIVE.API.DTO.ProfileUser
{
    public class ProfileUserEditDto
    {
        public int id_user { get; set; }

        public string? nm_user { get; set; }

        public string? pass_user { get; set; }

        public string? permission_user { get; set; }

        public string? status { get; set; }

        public CompanyVinculoDto Company { get; set; }
    }
}
