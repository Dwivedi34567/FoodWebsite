using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FoodOrderingWebsite.ViewModel
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }

        public string CategoryID { get; set; }
        public bool IsActive { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
