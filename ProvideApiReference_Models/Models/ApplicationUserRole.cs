using Microsoft.AspNetCore.Identity;

namespace ProvideApiReference_Models.Models
{
    public class ApplicationUserRole:IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
