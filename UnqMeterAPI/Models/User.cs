using Microsoft.AspNetCore.Identity;

namespace UnqMeterAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
    }
}
