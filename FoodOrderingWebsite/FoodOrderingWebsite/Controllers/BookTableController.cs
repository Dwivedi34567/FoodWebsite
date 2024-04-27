using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingWebsite.Controllers
{
    public class BookTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
