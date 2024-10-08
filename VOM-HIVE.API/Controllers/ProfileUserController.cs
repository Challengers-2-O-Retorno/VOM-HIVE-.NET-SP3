﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sp2.Models;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;
using VOM_HIVE.API.Services.Company;
using VOM_HIVE.API.Services.ProfileUser;

namespace VOM_HIVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUserController : ControllerBase
    {
        private readonly IProfileUserInterface _profileUserInterface;

        public ProfileUserController(IProfileUserInterface profileUserInterface)
        {
            _profileUserInterface = profileUserInterface;
        }

        [HttpGet("ListProfileUsers")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> ListProfileUsers()
        {
            var users = await _profileUserInterface.ListProfileUser();
            return Ok(users);
        }

        [HttpGet("FindProfilUserById/{id_user}")]
        public async Task<ActionResult<ResponseModel<ProfileuserModel>>> FindProfilUserById(int id_user)
        {
            var user = await _profileUserInterface.FindProfileUserById(id_user);
            return Ok(user);
        }

        [HttpGet("FindProfileUserByIdCompany/{id_company}")]
        public async Task<ActionResult<ResponseModel<CompanyModel>>> FindProfileUserByIdCompany(int id_company)
        {
            var user = await _profileUserInterface.FindProfileUserByIdCompany(id_company);
            return Ok(user);
        }

        [HttpPost("CreateProfileUser")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> CreateProfileUser(ProfileUserCreateDto profileUserCreateDto)
        {
            var user = await _profileUserInterface.CreateProfileUser(profileUserCreateDto);
            return Ok(user);
        }

        [HttpPut("EditProfileUser")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> EditProfileUser(ProfileUserEditDto profileUserEditDto)
        {
            var user = await _profileUserInterface.EditProfileUser(profileUserEditDto);
            return Ok(user);
        }

        [HttpDelete("DeleteProfileUser")]
        public async Task<ActionResult<ResponseModel<List<ProfileuserModel>>>> EditProfileUser(int id_user)
        {
            var user = await _profileUserInterface.DeleteProfileUser(id_user);
            return Ok(user);
        }
    }
}
