using Microsoft.AspNetCore.Identity;

namespace Imtahan.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
