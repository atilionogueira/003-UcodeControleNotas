using System.ComponentModel.DataAnnotations;


namespace Ucode.Core.Emuns
{
    public enum EEnrollmentStatus
    {
        [Display(Name = "Ativo")]
        Active = 1,

        [Display(Name = "Cancelado")]
        Cancelled = 2,

        [Display(Name = "Pendente")]
        Pending = 3,

        [Display(Name = "Concluído")]
        Completed = 4,

        [Display(Name = "Suspenso")]
        Suspended = 5
    }

}
