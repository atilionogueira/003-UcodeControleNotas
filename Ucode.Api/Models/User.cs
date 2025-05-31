using Microsoft.AspNetCore.Identity;

namespace Ucode.Api.Models
{
    public class User : IdentityUser<long>    
    {        
      //  public List<IdentityRole<long>>? Roles { get; set; }
    }
}
