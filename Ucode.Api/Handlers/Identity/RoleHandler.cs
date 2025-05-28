using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Api.Models;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Role;


namespace Ucode.Api.Handlers.Identity
{
    public class RoleHandler : IRoleHandler
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;
        public RoleHandler(RoleManager<Role> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<PagedResponse<List<RoleResponse>>> GetAllAsync(GetAllRoleRequest request)
        {
            try
            {
                var query = _context.Roles
                    .AsNoTracking()
                    .OrderBy(u => u.Name);

                var roles = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                var result = roles.Select(u => new RoleResponse
                {
                    Id = u.Id,
                    Name = u.Name ?? string.Empty
                }).ToList();

                return new PagedResponse<List<RoleResponse>>(
                    result,
                    count,
                    request.PageNumber,
                    request.PageSize
                );
            }
            catch
            {
                return new PagedResponse<List<RoleResponse>>(null, 500, "Não foi possível consultar os usuários");
            }
        }

        public async Task<Response<RoleResponse>> GetByIdAsync(GetRoleByIdRequest request)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id.ToString());
                if (role == null)
                    return new Response<RoleResponse>(null, 404, "Role não encontrado");

                var response = new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name ?? string.Empty,
                };

                return new Response<RoleResponse>(response, 200, "Role criado com sucesso");
            }
            catch
            {
                return new Response<RoleResponse>(null, 500, "Erro ao buscar usuário");
            }
        }

        public async Task<Response<RoleResponse>> CreateAsync(CreateRoleRequest request)
        {
            try
            {
                var role = new Role
            {
             
                Name = request.Name,            
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
               
                return new Response<RoleResponse>(null, 400, errors);
            }

            var response = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty,
               
            };
            
            return new Response<RoleResponse>(response, 201, "Role criado com sucesso");
            }
            catch
            {
                return new Response<RoleResponse>(null, 500, "Erro ao criar role");
            }
}     
       
        public async Task<Response<RoleResponse>> UpdateAsync(UpdateRoleRequest request)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id.ToString());
                if (role == null)
                    return new Response<RoleResponse>(null, 404, "role não encontrado");

                role.Name = request.Name;               

                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new Response<RoleResponse>(null, 400, errors);
                }

                var response = new RoleResponse
                {
                    Id = role.Id,
                    Name= role.Name ?? string.Empty                   
                };

                return new Response<RoleResponse>(response, 200, "Role atualizado com sucesso");
            }
            catch
            {
                return new Response<RoleResponse>(null, 500, "Erro ao atualizar role");
            }
        }

        public async Task<Response<RoleResponse>> DeleteAsync(DeleteRoleRequest request)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id.ToString());
                
                if (role == null)
                    return new Response<RoleResponse>(null, 404, "role não encontrado");

                var result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return new Response<RoleResponse>(null, 400, errors);
                }

                return new Response<RoleResponse>(null, 204, "Role excluído com sucesso");
            }
            catch
            {
                return new Response<RoleResponse>(null, 500, "Erro ao excluir role");
            }
        }
    }
}
