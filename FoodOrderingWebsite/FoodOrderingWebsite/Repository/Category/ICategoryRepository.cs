using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.Category
{
    public interface ICategoryRepository
    {
        DataTable AddCategory(CategoryViewModel category);
        List<CategoryViewModel> GetCategoryList();
    }
}
