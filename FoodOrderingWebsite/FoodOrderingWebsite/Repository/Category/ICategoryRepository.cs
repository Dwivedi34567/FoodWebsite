using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.Category
{
    public interface ICategoryRepository
    {
        DataTable AddCategory(CategoryViewModel category);
        List<CategoryViewModel> GetCategoryList();
        DataTable EditCategory(CategoryViewModel category);
        CategoryViewModel GetCategoryById(int categoryId);

        byte[] GetCategoryImageById(int categoryId);

        bool DeleteCategory(int categoryId);
    }
}
