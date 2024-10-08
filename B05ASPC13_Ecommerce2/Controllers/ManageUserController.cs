using B05ASPC13_Ecommerce2.Data;

using B05ASPC13_Ecommerce2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using B05ASPC13_Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using NuGet.Packaging.Signing;


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
        [HttpGet]
        public IActionResult AssignRole()
        {
            var user = _context.Users.OrderBy(o => o.UserName).ToList();
            var roles = _context.Roles.OrderBy(o => o.Name).ToList();
            ViewBag.Roles = new SelectList( roles, "Name", "Name");
            ViewBag.USers = new SelectList(user,"Id", "UserName"); ;
            return View();
        
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string username, string rolename)
        {
            string errMessage = "";
            var user = _context.Users.Where(u => u.Id.Equals(username)).FirstOrDefault();
          var result=await  _userManager.AddToRoleAsync(user,rolename);
            if (result.Succeeded)
            {
                return RedirectToAction("UserwithRole");
            }
            if (result.Errors.Count() > 0)
            {
                foreach (var error in result.Errors)
                {
                    errMessage += $"{error.Code}- {error.Description}";
                }

                ViewBag.ErrorMessage = errMessage;
            }
            return View();
        }
        [Authorize]
        public IActionResult UserwithRole()
        {
            var user = from u in _context.Users
                       join ur in _context.UserRoles
                       on u.Id equals ur.UserId
                       join r in _context.Roles
                       on ur.RoleId equals r.Id
                       select new USerwithRole
                       { 
                           Email=u.Email,
                       FullName=u.FullName,
                       ProfilePic=u.ProfilePic,
                       Role=r.Name??"Not assigned"

                       };
                 
            return View(user.ToList());
        }
        [Authorize(Roles ="Admin")]
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
               

                string fileName = $" {userprofile.FullName}  {ext}";
                string path = Path.Combine(wwwRootPath, "wwwroot/Pictures", fileName);
                using (var fileStrem = new FileStream(path, FileMode.Create))
                {
                    
                    await profilepic.CopyToAsync(fileStrem);
                }
                
                user.ProfilePic= "/Pictures/"+ fileName;
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
