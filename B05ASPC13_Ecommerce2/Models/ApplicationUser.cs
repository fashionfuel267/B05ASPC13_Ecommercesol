using Microsoft.AspNetCore.Identity;

namespace B05ASPC13_Ecommerce2.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string ProfilePic { get; set; }
        public string FullName { get; set; }
    }
}
