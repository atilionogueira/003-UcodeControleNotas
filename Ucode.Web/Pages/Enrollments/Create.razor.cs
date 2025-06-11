using Microsoft.AspNetCore.Components;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Models;
using Ucode.Core.Handlers;
using MudBlazor;
using Ucode.Core.Requests.Students;
using Ucode.Core.Requests.Course;
using Ucode.Core.Emuns;

namespace Ucode.Web.Pages.Enrollments
{
    public partial class CreateEnrollmentPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public CreateEnrollmentRequest InputModel { get; set; } = new() 
        {
            Status = EEnrollmentStatus.Active
        };
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
            IsBusy = false;

            try
            {
                var studentRequest = new GetAllStudentRequest();
                var studentResult = await StudentHandler.GetAllAsync(studentRequest);
                if (studentResult.IsSuccess)
                {
                    Students = studentResult.Data ?? [];
                }

                var courseRequest = new GetAllCourseRequest();
                var courseResult = await CourseHandler.GetAllAsync(courseRequest);
                if (courseResult.IsSuccess)
                {
                    Courses = courseResult.Data ?? [];
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);                
            }
            finally 
            {
                IsBusy = true;
            }
        }
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await EnrollmentHandler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
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
    }
}
