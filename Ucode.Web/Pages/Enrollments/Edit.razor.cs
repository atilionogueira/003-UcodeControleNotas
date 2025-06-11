using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Requests.Students;

namespace Ucode.Web.Pages.Enrollments
{
    public partial class EditEnrollmentPage : ComponentBase
    {
        #region Properties
        [Parameter]
        public string Id { get; set; } = string.Empty;
        public bool IsBusy { get; set; } = false;
        public UpdateEnrollmentRequest InputModel { get; set; } = new();
        public List<Enrollment> Enrollments { get; set; } = [];
        public List<Student> Students { get; set; } = [];
        public List<Course> Courses { get; set; } = [];

        #endregion

        #region Services
        [Inject]
        public IEnrollmentHandler EnrollmentHandler { get; set; } = null!;
        [Inject]
        public IStudentHandler StudentHandler { get; set; } = null!;
        [Inject]
        public ICourseHandler CourseHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            await GetEnrollmentsByIdAsync();
            await GetStudentAsync();
            await GetCourseAsync();

            IsBusy = false;
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await EnrollmentHandler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Matrícula atualizada", Severity.Success);
                    NavigationManager.NavigateTo("/enrollments");
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region methods private

        private async Task GetEnrollmentsByIdAsync() 
        {
            IsBusy = true;
            try
            {
                var request = new GetEnrollmentsByIdRequest { Id = long.Parse(Id) };
                var result = await EnrollmentHandler.GetByIdAsync(request);
                if (result is { IsSuccess: true, Data: not null }) 
                {
                    InputModel = new UpdateEnrollmentRequest
                    {
                        Id = result.Data.Id,
                        CourseId = result.Data.CourseId,
                        StudentId = result.Data.StudentId,
                        EnrollmentNumber = result.Data.EnrollmentNumber,
                        Status = result.Data.Status 
                    };
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally 
            {
                IsBusy = false;
            }
        }

        private async Task GetStudentAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllStudentRequest();
                var result = await StudentHandler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Students = result.Data ?? [];
                    InputModel.StudentId = Students.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetCourseAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCourseRequest();
                var result = await CourseHandler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Courses = result.Data ?? [];
                    InputModel.CourseId = Courses.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
