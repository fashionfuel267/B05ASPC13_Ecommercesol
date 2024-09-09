using B05ASPC13_Ecommerce2.Data;

using B05ASPC13_Ecommerce2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using B05ASPC13_Ecommerce2.Models;


namespace B05ASPC13_Ecommerce2.Controllers
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
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(USerwithRole userprofile,IFormFile profilepic)
        {
        
            if (profilepic == null) {
                ModelState.AddModelError("", "Please provide valid picture");
                return View(userprofile);
            }
            string wwwRootPath = "";
            string rpath = "";
          //wwwRootPath = Directory.GetCurrentDirectory();
          // rpath = Path.Combine(wwwRootPath, "/wwwroot/Pictures");
            if (env != null)
            {
                wwwRootPath = env.ContentRootPath;
                rpath = wwwRootPath + "/Pictures";
            }
            else
            {
                wwwRootPath = Directory.GetCurrentDirectory();
                rpath = Path.Combine(wwwRootPath, "wwwroot/Pictures");
            }
            var uname = User.Identity.Name;
            var user = _userManager.Users.Where(u=>u.UserName.Equals(uname)).FirstOrDefault();
            string ext= Path.GetExtension(profilepic.FileName).ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                string fileToSave =Path.Combine(wwwRootPath, "wwwroot/Pictures", userprofile.FullName + ext);
               using(FileStream f=new FileStream(fileToSave, FileMode.Create))
                {
                  await  profilepic.CopyToAsync(f);

                }

                user.ProfilePic= "/Pictures/"+ profilepic.FileName;
                user.FullName= userprofile.FullName;

            var result= await   _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("UserwithRole");
                }
                if(result.Errors.Count()>0)
                {

                }

            }
            else {
                ModelState.AddModelError("", "Please provide valid picture");
                return View(userprofile);
            }
            return View();
        }
    }
}
