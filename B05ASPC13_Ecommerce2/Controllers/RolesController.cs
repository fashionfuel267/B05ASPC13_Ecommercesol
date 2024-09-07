using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B05ASPC13_Ecommerce2.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var model= _roleManager.Roles.ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View( );
        }
        [HttpPost]
        public async Task<IActionResult> Create(string RoleName)
        {
            string errMessage = "";
            var result= await   _roleManager.CreateAsync(new IdentityRole { Name= RoleName });
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            if(result.Errors.Count()>0)
            {
                foreach (var error in result.Errors) {
                    errMessage += $"{error.Code}- {error.Description}";
                }
                
                ViewBag.ErrorMessage = errMessage;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model =await  _roleManager.FindByIdAsync(id );
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            string errMessage = "";
            var model = await _roleManager.FindByIdAsync(role.Id);
            model.Name=role.Name;
            model.Id=role.Id;   
            var result = await _roleManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
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
    

    public async Task<IActionResult> Delete(string id)
    {
        string errMessage = "";
        var model = await _roleManager.FindByIdAsync(id.ToString());
        var result=await    _roleManager.DeleteAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            {
                foreach (var error in result.Errors)
                {
                    errMessage += $"{error.Code}- {error.Description}";
                }

                ViewBag.ErrorMessage = errMessage;
            }

            return View("Index");
    }
}
}
