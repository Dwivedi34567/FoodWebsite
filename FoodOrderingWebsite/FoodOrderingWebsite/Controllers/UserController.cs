using FoodOrderingWebsite.Repository.Product;
using FoodOrderingWebsite.Repository.User;
using FoodOrderingWebsite.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            try
            {
                if(ModelState.IsValid) 
                {
                    _userRepository.AddUser(register);
                    ViewBag.IsSuccess = true;
                    return View("Index");
                }
                return View("Index",register);
               
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ViewBag.IsSuccess = false;
                ViewBag.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View("Index",register);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
