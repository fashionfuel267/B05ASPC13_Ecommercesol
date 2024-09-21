using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B05ASPC13_Ecommerce2.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult un()
        {
            return View();
        }
    }
}
