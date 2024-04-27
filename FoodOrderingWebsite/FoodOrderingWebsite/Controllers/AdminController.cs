using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingWebsite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
