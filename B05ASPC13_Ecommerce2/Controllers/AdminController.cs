using Microsoft.AspNetCore.Mvc;

namespace B05ASPC13_Ecommerce2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
