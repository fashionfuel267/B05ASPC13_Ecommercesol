using B05ASPC13_Ecommerce.Data;

using B05ASPC13_Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using B05ASPC13_Ecommerce.Models;

namespace B05ASPC13_Ecommerce.Controllers
{
    public class ManageUserController : Controller
    {
        IHostEnvironment env;
        UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _context;
        public ManageUserController(IHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.env = env;
            this._userManager = userManager;
            this._context = db;
        }
        [Authorize]
        public IActionResult UserwithRole()
        {
            return View();
        }
        [Authorize]
        public IActionResult Create()
        {
            return View("");
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(USerwithRole userprofile,IFormFile profilepic)
        {
        
            if (profilepic == null) {
                ModelState.AddModelError("", "Please provide valid picture");
                return View(userprofile);
            }
            var uname = User.Identity.Name;
            var user = _userManager.Users.Where(u=>u.UserName.Equals(uname));
            string ext= Path.GetExtension(profilepic.FileName).ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                string fileToSave =Path.Combine( env.ContentRootPath, "Pictures");
               using(FileStream f=new FileStream(fileToSave, FileMode.Create))
                {
                    profilepic.CopyToAsync(f);

                }
                //user.ProfilePic= "/Pictures/"+ profilepic.FileName;
                //user.FullName= profilepic.FileName;
                //_userManager.UpdateAsync();

            }
            else {
                ModelState.AddModelError("", "Please provide valid picture");
                return View(userprofile);
            }
            return View();
        }
    }
}
