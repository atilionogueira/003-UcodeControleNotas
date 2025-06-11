using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses.Enrollment;

namespace Ucode.Web.Pages.Enrollments
{
    public partial class ListEnrollmentPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<EnrollmentListResponse> Enrollments { get; set; } = new List<EnrollmentListResponse>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public IEnrollmentHandler Handler { get; set; } = null!;    
        #endregion

        #region Override

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {                

                var request = new GetAllEnrollmentRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Enrollments = result.Data ?? [];

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
        #region Methods
        public async void OnDeleteButtonClickedAsync(long id)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir o lançamento será excluido. Esta ação é irreversível! Deseja Continuar",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");
            if (result is true)
                await OnDeleteAsync(id);
            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id)
        {
            try
            {
                var request = new DeleteEnrollmentsRequest { Id = id };
                await Handler.DeleteAsync(request);
                Enrollments.RemoveAll(x => x.Id == id);
                Snackbar.Add("Matricula excluída", Severity.Success);
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
        public Func<EnrollmentListResponse, bool> Filter => enrollment =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (enrollment.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (!string.IsNullOrEmpty(enrollment.StudentName) && enrollment.StudentName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    return true;

            if (!string.IsNullOrEmpty(enrollment.CourseName) && enrollment.CourseName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;          

            return false;

        };
        #endregion
    }
}
