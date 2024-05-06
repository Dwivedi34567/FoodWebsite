using FoodOrderingWebsite.Repository.Category;
using FoodOrderingWebsite.Repository.Product;
using FoodOrderingWebsite.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace FoodOrderingWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            List< ProductViewModel> categoryList = _productRepository.GetAllCategories();
            // Convert the list of categories to a list of SelectListItem
          
            ProductViewModel model = new ProductViewModel();
            model.ProductCategoryList = categoryList;
            return View(model);
        }

        public IActionResult ProductList()
        {
            List<ProductViewModel> productList = _productRepository.GetProductList();
            ProductViewModel model = new ProductViewModel();
            model.ProductList = productList;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel product, IFormFile ProductImage)
        {
            try
            {
                ModelState.Remove("ProductCategory");
                ModelState.Remove("ProductCategoryList");
                ModelState.Remove("ProductImage");
                ModelState.Remove("ProductList");

                if (ModelState.IsValid)
                {
                    // Convert uploaded image to byte array
                    if (ProductImage != null && ProductImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ProductImage.CopyTo(memoryStream);
                            product.ProductImage = memoryStream.ToArray();
                        }
                    }

                    DataTable results = _productRepository.AddProduct(product);
                    if (results != null && results.Rows.Count == 0)
                    {
                        ViewBag.IsSuccess = true;
                    }
                }
                // If validation fails, return the form with error messages
                return View("Index", product);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("Getting some errors" + ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.IsSuccess = false;
                return View("Index", product);
            }
        }

        public IActionResult EditProduct(int productId)
        {
            try
            {
                ProductViewModel product = _productRepository.GetProductById(productId);
                List<ProductViewModel> categoryList = _productRepository.GetAllCategories();
                product.ProductCategoryList = categoryList;
                return View(product);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel product, IFormFile ProductImage)
        {
            try
            {
                // Convert uploaded image to byte array
                if (ProductImage != null && ProductImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ProductImage.CopyTo(memoryStream);
                        product.ProductImage = memoryStream.ToArray();
                    }
                }
                else
                {
                    byte[] imageUrl = _productRepository.GetProductImageById(product.ProductId);
                    product.ProductImage = imageUrl;
                }

                DataTable results = _productRepository.EditProduct(product);
                if (results != null && results.Rows.Count == 0)
                {
                    ViewBag.IsSuccess = true;
                }
                return View("EditProduct", product);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                bool isDeleted = _productRepository.DeleteProduct(productId);
                if (isDeleted)
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
