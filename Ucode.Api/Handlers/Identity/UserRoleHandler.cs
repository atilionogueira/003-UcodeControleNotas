using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Models;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Api.Handlers.Identity
{
    public class UserRoleHandler(UserManager<User> userManager, RoleManager<Role> roleManager) : IUserRoleHandler
    {
        public async Task<Response<UserRoleResponse>> CreateAsync(CreateUserRoleRequest request)
        {
            var user = await userManager.FindByIdAsync(request.TargetUserId.ToString());
            if (user is null)
                return new Response<UserRoleResponse>(null, 404, "Usuário não encontrado");

            var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role is null)
                return new Response<UserRoleResponse>(null, 404, "Role não encontrada");

            var result = await userManager.AddToRoleAsync(user, role.Name!);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new Response<UserRoleResponse>(null, 400, $"Erro ao adicionar role ao usuário: {errors}");
            }

            return new Response<UserRoleResponse>(
                new UserRoleResponse
                {
                    UserId = user.Id,
                    RoleId = role.Id,
                    RoleName = role.Name = string.Empty
                },
                200,
                "Role atribuída com sucesso ao usuário"
            );
        }
        /*
        public async Task<Response<GetUserRolesResponse>> GetUserRolesAsync(GetUserRolesRequest request)
        {
            var user = await userManager.FindByIdAsync(request.TargetUserId.ToString());
            if (user is null)
                return new Response<GetUserRolesResponse>(null,500,"Usuário não encontrado");

            //var allRoles = roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await userManager.GetRolesAsync(user);

            var response = new GetUserRolesResponse
            {
                Id = user.Id,
                Roles = userRoles.Select(role => new RoleAssignmentResponse
               //Roles = allRoles.Select(role => new RoleAssignmentResponse
                {
                    RoleName = role!,
                   // IsAssigned = userRoles.Contains(role!)
                   IsAssigned = true
                }).ToList()
            };

            return new Response<GetUserRolesResponse>(response);
        }
        */

        public async Task<Response<GetUserRolesResponse>> GetUserRolesAsync(GetUserRolesRequest request)
        {
            var user = await userManager.FindByIdAsync(request.TargetUserId.ToString());
            if (user is null)
                return new Response<GetUserRolesResponse>(null, 404, "Usuário não encontrado");

            // Todas as roles existentes no sistema
            var allRoles = await roleManager.Roles.Select(r => r.Name!).ToListAsync();

            // Roles que o usuário já possui
            var userRoles = await userManager.GetRolesAsync(user);

            var response = new GetUserRolesResponse
            {
                Id = user.Id,
                Roles = allRoles.Select(role => new RoleAssignmentResponse
                {
                    RoleName = role,
                    IsAssigned = userRoles.Contains(role)
                }).ToList()
            };

            return new Response<GetUserRolesResponse>(response);
        }

        public async Task<Response<GetUserRolesResponse>> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            var user = await userManager.FindByIdAsync(request.TargetUserId.ToString());
            if (user is null)
                return new Response<GetUserRolesResponse>(null, 404, "Usuário não encontrado");

            var currentRoles = await userManager.GetRolesAsync(user);

            // Remover roles antigas
            if (currentRoles.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                    return new Response<GetUserRolesResponse>(null, 400, $"Erro ao remover roles atuais: {errors}");
                }
            }

            // Adicionar novas roles se houver
            if (request.Roles is not null && request.Roles.Any())
            {
                var addResult = await userManager.AddToRolesAsync(user, request.Roles);
                if (!addResult.Succeeded)
                {
                    var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    return new Response<GetUserRolesResponse>(null, 400, $"Erro ao adicionar novas roles: {errors}");
                }
            }

            // Retornar resultado atualizado
            return await GetUserRolesAsync(new GetUserRolesRequest { TargetUserId = request.TargetUserId });
        }

    }
}
