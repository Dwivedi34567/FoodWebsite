using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FoodOrderingWebsite.ViewModel
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Category is required")]
        public string CategoryName { get; set; }

        public string CategoryID { get; set; }
    }
}
