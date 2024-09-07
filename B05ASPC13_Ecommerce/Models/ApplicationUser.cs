using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace B05ASPC13_Ecommerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(150)]
        public string ProfilePic { get; set; }
        [StringLength(30)]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
    }
}
