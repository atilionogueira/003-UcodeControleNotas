using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Enrollment;

namespace Ucode.Api.Handlers
{
    public class EnrollmentHandler(AppDbContext context) : IEnrollmentHandler
    {

        public async Task<PagedResponse<List<EnrollmentListResponse>>> GetAllAsync(GetAllEnrollmentRequest request)
        {
            try
            {
                var query = context
               .Enrollments
               .Include(e => e.Student)
               .Include(e => e.Course)
               .AsNoTracking()
               .Where(x => x.UserId == request.UserId);
            
                var enrollment = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                     .Select(e => new EnrollmentListResponse
                     {
                         Id = e.Id,
                         StudentName = e.Student.Name,
                         CourseName = e.Course.Name,
                         Status = e.Status
                         
                     })
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<EnrollmentListResponse>>(enrollment, count, request.PageNumber, request.PageSize);

            }
            catch
            {
                return new PagedResponse<List<EnrollmentListResponse>>(null, 500, "Não foi possível consultar as matriculas");
            }

        }
        public async Task<Response<Enrollment?>> GetByIdAsync(GetEnrollmentsByIdRequest request)
        {
            try
            {
                var enrollment = await context
               .Enrollments
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return enrollment is null
                    ? new Response<Enrollment?>(null, 404, "Matricula não encontrada")
                    : new Response<Enrollment?>(enrollment);
            }
            catch
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível encontrar Matrícula");
            }           
        }       
        public async Task<Response<Enrollment?>> CreateAsync(CreateEnrollmentRequest request)
        {
            try
            {
                var enrollment = new Enrollment
                {
                    UserId = request.UserId,
                    EnrollmentNumber = Guid.NewGuid().ToString("N")[..8],
                    CourseId = request.CourseId,
                    StudentId = request.StudentId,
                    Status = request.Status,
                    IsActive = request.IsActive,
                    CreatedAt = DateTime.Now,
                };

                await context.Enrollments.AddAsync(enrollment);
                await context.SaveChangesAsync();

                return new Response<Enrollment?>(enrollment, 201, "Matrícula criado com sucesso");
            }
            catch
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível criar o matrícula");
            }
        }
        public async Task<Response<Enrollment?>> UpdateAsync(UpdateEnrollmentRequest request)
        {
            try
            {
                var enrollment = await context
                .Enrollments
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (enrollment is null)
                    return new Response<Enrollment?>(null, 404, "Matrícula nao encontrado");

                enrollment.CourseId = request.CourseId;
                enrollment.StudentId = request.StudentId;
                enrollment.Status = request.Status;
                enrollment.IsActive = request.IsActive;
                enrollment.UpdatedAt = DateTime.Now;
                enrollment.UserId = request.UserId;

                context.Enrollments.Update(enrollment);
                await context.SaveChangesAsync();

                return new Response<Enrollment?>(enrollment, message: "Matrícula atualizado com sucesso");
            }
            catch 
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível alterar o Enrollment");
            }
        }
        public async Task<Response<Enrollment?>> DeleteAsync(DeleteEnrollmentsRequest request)
        {
            var enrollment = await context
                .Enrollments
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (enrollment is null)
                return new Response<Enrollment?>(null, 404, "Matricula não encontrada");
            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();

            return new Response<Enrollment?>(enrollment, 404, "Matricula excluida com sucesso");
        }

        

        

        
    }
}
