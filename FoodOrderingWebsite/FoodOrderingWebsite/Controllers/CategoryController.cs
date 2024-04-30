using FoodOrderingWebsite.Repository.Category;
using FoodOrderingWebsite.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;

namespace FoodOrderingWebsite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var model = new CategoryViewModel();
            model.CategoryList = _categoryRepository.GetCategoryList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel category, IFormFile CategoryImage)
        {
            try
            {
                ModelState.Remove("CategoryID");
                ModelState.Remove("CategoryList");
                ModelState.Remove("ImageData");

                if (ModelState.IsValid)
                {
                    // Convert uploaded image to byte array
                    if (CategoryImage != null && CategoryImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            CategoryImage.CopyTo(memoryStream);
                            category.ImageData = memoryStream.ToArray();
                        }
                    }

                    DataTable results = _categoryRepository.AddCategory(category);
                    if (results != null && results.Rows.Count == 0)
                    {
                        ViewBag.IsSuccess = true;
                        var model = new CategoryViewModel();
                        model.CategoryList = _categoryRepository.GetCategoryList();
                        return View("Index",model);
                    }
                }
                // If validation fails, return the form with error messages
                return View("Index", category);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("Getting some errors" + ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.IsSuccess = false;
                return View("Index", category);
            }
        }

     
        public IActionResult EditCategory(int categoryId)
        {
            try
            {
                CategoryViewModel category = _categoryRepository.GetCategoryById(categoryId);
                return View(category);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel category, IFormFile CategoryImage)
        {
            try
            {
                    // Convert uploaded image to byte array
                    if (CategoryImage != null && CategoryImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            CategoryImage.CopyTo(memoryStream);
                            category.ImageData = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                      byte[] imageUrl = _categoryRepository.GetCategoryImageById(category.CategoryID);
                      category.ImageData = imageUrl;
                    }

                    DataTable results = _categoryRepository.EditCategory(category);
                    if (results != null && results.Rows.Count == 0)
                    {
                        ViewBag.IsSuccess = true;
                    }
                return View("EditCategory",category);
            }
            catch 
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                bool isDeleted = _categoryRepository.DeleteCategory(categoryId);
                if(isDeleted)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

    }
}
