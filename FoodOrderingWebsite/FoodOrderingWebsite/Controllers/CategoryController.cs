using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingWebsite.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
