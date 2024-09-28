using B05ASPC13_Ecommerce2.Data;
using B05ASPC13_Ecommerce2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B05ASPC13_Ecommerce2.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        
        public TeacherController(ApplicationDbContext context, IWebHostEnvironment en)
        {
            _context = context;
            _environment = en;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var model = _context.Instructors.Where(i => i.Email.Equals(User.Identity.Name)).FirstOrDefault();
            return View(model?? new Instructor { Email=User.Identity.Name});
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ChangePic(IFormFile Pic)
        {
            string msg = "";
            string rPath = "";
;            string wwwRootPath = "";
            if (_environment != null)
            {
                wwwRootPath = _environment.WebRootPath;
                rPath = wwwRootPath + "/Instrcutor";
            }
            else
            {
                wwwRootPath = Directory.GetCurrentDirectory();
                rPath = Path.Combine(wwwRootPath, "/wwwroot/Instrcutor");
            }

            if (Pic !=null)
            {
                    string ext= Path.GetExtension(Pic.FileName).ToLower() ;
                if(ext ==".jpg"|| ext==".png" || ext == ".jpeg")
                {

                    //string fileName=
                    string path = Path.Combine(rPath, Pic.FileName);
                    using (var fileStrem = new FileStream(path, FileMode.Create))
                    {
                       await  Pic.CopyToAsync(fileStrem);
                    }
                    var existing = _context.Instructors.Where(i => i.Email.Equals(User.Identity.Name)).FirstOrDefault();
                    if (existing != null) {
                        existing.Picpath = "/Instrcutor/" + Pic.FileName;
                        _context.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        
                    }
                    else
                    {
                        var instructor = new Instructor
                        {
                            Email = User.Identity.Name,
                            Picpath = "/Instructor/" + Pic.FileName
                        };
                        _context.Instructors.Add(instructor);
                    }
                   if(_context.SaveChanges() > 0)
                    {
                        return RedirectToAction("Profile");
                    }

                }
            }
            else
            {
                msg = "Please provide valid Picture";
                ViewBag.msg = msg;
            }
            ViewBag.msg = msg;
            return View("Profile");
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            try
            {
                if(instructor.Id>0)
                {
                    _context.Entry(instructor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    _context.Instructors.Add(instructor);
                }
                
                if (_context.SaveChanges()>0) {
                    return RedirectToAction("Profile");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return View("Profile",instructor);
        }

    }
}
