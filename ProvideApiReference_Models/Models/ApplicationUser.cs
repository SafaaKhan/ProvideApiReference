using Microsoft.AspNetCore.Identity;

namespace ProvideApiReference_Models.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
