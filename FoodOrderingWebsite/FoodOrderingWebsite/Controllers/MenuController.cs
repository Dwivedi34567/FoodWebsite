using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingWebsite.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}
