using Microsoft.AspNetCore.Identity;

namespace EventTicketing.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int MyProperty { get; set; }
    }
}
