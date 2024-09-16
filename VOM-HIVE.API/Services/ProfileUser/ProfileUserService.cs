using Microsoft.EntityFrameworkCore;
using Sp2.Models;
using System.Collections.Generic;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.DTO.Company;
using VOM_HIVE.API.DTO.ProfileUser;
using VOM_HIVE.API.Models;

namespace VOM_HIVE.API.Services.ProfileUser
{
    public class ProfileUserService : IProfileUserInterface
    {
        private readonly AppDbContext _context;

        public ProfileUserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<List<ProfileuserModel>>> ListProfileUser()
        {
            ResponseModel<List<ProfileuserModel>> resposta = new ResponseModel<List<ProfileuserModel>>();
            try
            {
                var user = await _context.Profile_user.Include(c => c.Company).ToListAsync();

                resposta.Dados = user;
                resposta.Mensagem = "Todos os usuários foram coletados!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ProfileuserModel>> FindProfileUserById(int id_user)
        {
            ResponseModel<ProfileuserModel> resposta = new ResponseModel<ProfileuserModel>();

            try
            {
                var user = await _context.Profile_user.Include(c => c.Company).FirstOrDefaultAsync(userDb => userDb.id_user == id_user);

                if (user == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizada!";
                    return resposta;
                }

                resposta.Dados = user;
                resposta.Mensagem = "Usuário localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProfileuserModel>>> FindProfileUserByIdCompany(int id_company)
        {
            ResponseModel<List<ProfileuserModel>> resposta = new ResponseModel<List<ProfileuserModel>>();

            try
            {
                var user = await _context.Profile_user
                    .Include(pu => pu.Company)
                    .Where(userDb => userDb.Company.id_company == id_company)
                    .ToListAsync();

                if (user == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizada!";
                    return resposta;
                }

                resposta.Dados = user;
                resposta.Mensagem = "Usuário localizado!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProfileuserModel>>> CreateProfileUser(ProfileUserCreateDto ProfileUserCreateDto)
        {
            ResponseModel<List<ProfileuserModel>> resposta = new ResponseModel<List<ProfileuserModel>>();

            try
            {
                var company = await _context.Company.FirstOrDefaultAsync(companyDb => companyDb.id_company == ProfileUserCreateDto.Company.id_company);

                if (company == null)
                {
                    resposta.Mensagem = "Nenhum registro de empresa localizado!";
                    return resposta;
                }

                var user = new ProfileuserModel()
                {
                    nm_user = ProfileUserCreateDto.nm_user,
                    pass_user = ProfileUserCreateDto.pass_user,
                    dt_register = DateTime.Now,
                    permission_user = ProfileUserCreateDto.permission_user,
                    status = ProfileUserCreateDto.status,
                    Company = company
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Profile_user.Include(co => co.Company).ToListAsync();
                resposta.Mensagem = "Usuário criado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProfileuserModel>>> DeleteProfileUser(int id_user)
        {
            ResponseModel<List<ProfileuserModel>> resposta = new ResponseModel<List<ProfileuserModel>>();
            try
            {
                var user = await _context.Profile_user.FirstOrDefaultAsync(userDb => userDb.id_user == id_user);

                if (user == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizado!";
                    return resposta;
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Profile_user.ToListAsync();
                resposta.Mensagem = "Usuário removido com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ProfileuserModel>>> EditProfileUser(ProfileUserEditDto ProfileUserEditDto)
        {
            ResponseModel<List<ProfileuserModel>> resposta = new ResponseModel<List<ProfileuserModel>>();
            try
            {
                var user = await _context.Profile_user
                    .Include(co => co.Company)
                    .FirstOrDefaultAsync(userDb => userDb.id_user == ProfileUserEditDto.id_user);

                var company = await _context.Company
                    .FirstOrDefaultAsync(companyDb => companyDb.id_company == ProfileUserEditDto.Company.id_company);

                if (user == null)
                {
                    resposta.Mensagem = "Nenhum registro de usuário localizado!";
                    return resposta;
                }

                if (company == null)
                {
                    resposta.Mensagem = "Nenhum registro de empresa localizado!";
                    return resposta;
                }

                user.nm_user = ProfileUserEditDto.nm_user;
                user.pass_user = ProfileUserEditDto.pass_user;
                user.permission_user = ProfileUserEditDto.permission_user;
                user.status = ProfileUserEditDto.status;
                user.Company = company;

                _context.Update(user);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Profile_user.ToListAsync();
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
