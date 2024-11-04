using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.Auth;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.ProfileUser;

namespace VOM_HIVE.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUserController : ControllerBase
    {
        private readonly IProfileUserInterface _profileUserInterface;
        private readonly IAuthenticateInterface _authenticateInterface;

        public ProfileUserController(IProfileUserInterface profileUserInterface, IAuthenticateInterface authenticateInterface)
        {
            _profileUserInterface = profileUserInterface;
            _authenticateInterface = authenticateInterface;
        }

        [HttpGet("ListProfileUsers")]
        [EndpointDescription("Endpoint responsável por listar todos os usuários cadastrados.")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> ListProfileUsers()
        {
            var users = await _profileUserInterface.ListProfileUser();
            return Ok(users);
        }

        [HttpGet("FindProfilUserById/{id_user}")]
        [EndpointDescription("Endpoint responsável por listar um usuário específico de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<ProfileuserModel>>> FindProfilUserById(int id_user)
        {
            var user = await _profileUserInterface.FindProfileUserById(id_user);
            return Ok(user);
        }

        [HttpGet("FindProfileUserByIdCompany/{id_company}")]
        [EndpointDescription("Endpoint responsável por listar um usuário específico de acordo com o Id de empresa.")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindProfileUserByIdCompany(int id_company)
        {
            var user = await _profileUserInterface.FindProfileUserByIdCompany(id_company);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("CreateProfileUser")]
        [EndpointDescription("Endpoint responsável por criar um novo usuário.")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> CreateProfileUser(ProfileUserCreateDto profileUserCreateDto)
        {
            var userExist = await _authenticateInterface.userExists(profileUserCreateDto.nm_user);
            if (userExist)
            {
                return BadRequest("Este usuário já possui um cadastro.");
            }

            var user = await _profileUserInterface.CreateProfileUser(profileUserCreateDto);
            return Ok(user);
        }

        [HttpPut("EditProfileUser/{id_user}")]
        [EndpointDescription("Endpoint responsável por editar um usuário de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> EditProfileUser(int id_user, [FromBody] ProfileUserEditDto profileUserEditDto)
        {
            if (id_user != profileUserEditDto.id_user)
            {
                return BadRequest("Id na URL e no corpo não coincidem");
            }

            var user = await _profileUserInterface.EditProfileUser(profileUserEditDto);

            if (user.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeleteProfileUser/{id_user}")]
        [EndpointDescription("Endpoint responsável por deletar um usuário de acordo com o Id.")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> EditProfileUser(int id_user)
        {
            var user = await _profileUserInterface.DeleteProfileUser(id_user);

            if (user.Dados == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
