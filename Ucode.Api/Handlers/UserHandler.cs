
using Microsoft.AspNetCore.Identity;
using Ucode.Core.Handlers;
using Ucode.Api.Models;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Ucode.Api.Handlers
{
    public class UserHandler(UserManager<User> userManager, IHttpContextAccessor _httpContextAccessor) : IUserHandler
    {
        public async Task<PagedResponse<List<UserResponse>>> GetAllAsync(GetAllUsersRequest request)
        {
            try
            {
                var query = userManager.Users
                    .AsNoTracking()
                    .OrderBy(u => u.UserName);

                var users = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                var result = users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email ?? string.Empty,
                    UserName = u.UserName ?? string.Empty
                }).ToList();

                return new PagedResponse<List<UserResponse>>(
                    result,
                    count,
                    request.PageNumber,
                    request.PageSize
                );
            }
            catch
            {
                return new PagedResponse<List<UserResponse>>(null, 500, "Não foi possível consultar os usuários");
            }
        }

        public async Task<Response<UserResponse>> GetByIdAsync(GetUserByIdRequest request)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado");

                var response = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber
                };

                return new Response<UserResponse>(response, 200);
            }
            catch
            {
                return new Response<UserResponse>(null, 500, "Erro ao buscar usuário");
            }
        }
        public async Task<Response<UserResponse>> CreateAsync(CreateUserRequest request)
        {
            try
            {
                // 👉 Recuperar o ID do usuário logado (vem do token ou cookie)
                var userId = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return new Response<UserResponse>(null, 401, "Usuário não autenticado");

                // Se quiser converter pra long:
                // var id = long.Parse(userId);

                var existingUser = await userManager.FindByNameAsync(request.UserName);
                if (existingUser is not null)
                    return new Response<UserResponse>(null, 400, "Já existe um usuário com este nome");

                var user = new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new Response<UserResponse>(null, 400, errors);
                }

                var response = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber
                };

                return new Response<UserResponse>(response, 201, "Usuário criado com sucesso");
            }
            catch
            {
                return new Response<UserResponse>(null, 500, "Erro ao criar usuário");
            }
        }

        public async Task<Response<UserResponse>> UpdateAsync(UpdateUserRequest request)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado");

                user.UserName = request.UserName;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;

                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new Response<UserResponse>(null, 400, errors);
                }

                var response = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber
                };

                return new Response<UserResponse>(response, 200, "Usuário atualizado com sucesso");
            }
            catch
            {
                return new Response<UserResponse>(null, 500, "Erro ao atualizar usuário");
            }
        }

        public async Task<Response<UserResponse>> DeleteAsync(DeleteUserRequest request)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado");

                var result = await userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new Response<UserResponse>(null, 400, errors);
                }

                return new Response<UserResponse>(null, 204, "Usuário excluído com sucesso");
            }
            catch
            {
                return new Response<UserResponse>(null, 500, "Erro ao excluir usuário");
            }
        }

       

      
    }
}
