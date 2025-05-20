
namespace Ucode.Core.Responses.Account.User
{
    public class UserResponse
    {
        public long Id { get; set; } // IdentityUser.Id é long
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}
