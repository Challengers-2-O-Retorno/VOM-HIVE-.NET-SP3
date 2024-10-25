using Sp2.Models;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.ProfileUser
{
    public interface IProfileUserInterface
    {
        Task<ResponseModel<List<ProfileuserModel>>> ListProfileUser();
        Task<ResponseModel<ProfileuserModel>> FindProfileUserById(int id_user);
        Task<ResponseModel<List<ProfileuserModel>>> FindProfileUserByIdCompany(int id_company);
        Task<ResponseModel<ProfileuserModel>> CreateProfileUser(ProfileUserCreateDto ProfileUserCreateDto);
        Task<ResponseModel<ProfileuserModel>> EditProfileUser(ProfileUserEditDto ProfileUserEditDto);
        Task<ResponseModel<ProfileuserModel>> DeleteProfileUser(int id_user);
    }
}
