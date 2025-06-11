using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class CourseHandler(AppDbContext context) : ICourseHandler
    {
        public async Task<PagedResponse<List<Course>>> GetAllAsync(GetAllCourseRequest request)
        {
            try
            {
                var query = context
                    .Courses
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Name);

                var courses = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Course>>(courses,count,request.PageNumber,request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Course>>(null, 500, "Não foi possível consultar os cursos");
            }

        }


        public async Task<Response<Course?>> GetByIdAsync(GetCourseByRequest request)
        {
            try
            {
                var course = await context
                 .Courses
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return course is null
                    ? new Response<Course?>(null, 404, "Curso não encontrado")
                    : new Response<Course?>(course);
            }
            catch
            {
                return new Response<Course?>(null, 500, "Não foi possivel recuperar o curso");
            }
        }

        public async Task<Response<Course?>> CreateAsync(CreateCourseRequest request)
        {
            try
            {
                var course = new Course
                {
                    UserId = request.UserId,
                    Name = request.Name,
                    Description = request.Description,
                    DurationInHours = request.DurationInHours                   
                };

                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();

                return new Response<Course?>(course, 201, "Curso criado com sucesso");
            }
            catch
            {
                return new Response<Course?>(null, 500, "Não foi possível criar o curso");
            }
        }

        public async Task<Response<Course?>> UpdateAsync(UpdateCourseRequest request)
        {
            try
            {
                var course = await context
               .Courses
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (course is null)
                    return new Response<Course?>(null, 404, "Curso não encontrado");

                course.Name = request.Name;
                course.Description = request.Description;
                course.DurationInHours = request.DurationInHours;
                course.UpdatedAt = DateTime.Now;

                context.Courses.Update(course);
                await context.SaveChangesAsync();

                return new Response<Course?>(course, message: "Curso atualizado com sucesso");

            }
            catch
            {
                return new Response<Course?>(null, 500, "Não foi possível atualizar o curso");
            }
        }

        public async Task<Response<Course?>> DeleteAsync(DeleteCourseRequest request)
        {
            try
            {
                var course = await context
                    .Courses
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (course is null)
                    return new Response<Course?>(null, 404, "Curso não encontrado");

                context.Courses.Remove(course);
                await context.SaveChangesAsync();

                return new Response<Course?>(course, message: "Curso excluído com sucesso");
            }
            catch
            {
                return new Response<Course?>(null, 500, "Não foi possível excluir o curso");
            }
        }

    }
}
