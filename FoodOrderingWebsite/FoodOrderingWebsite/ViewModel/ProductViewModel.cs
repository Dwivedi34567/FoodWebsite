using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingWebsite.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Product description is required")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid quantity")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Product category is required")]
        public string ProductCategory { get; set; }

        public bool IsActive { get; set; }

        public byte[] ProductImage { get; set; }

        public List<ProductViewModel> ProductList { get; set; }
        public List<ProductViewModel> ProductCategoryList { get; set; }
    }
}
