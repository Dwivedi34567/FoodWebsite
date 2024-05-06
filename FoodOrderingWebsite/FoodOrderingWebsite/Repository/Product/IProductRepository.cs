using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.Product
{
    public interface IProductRepository
    {
        List<ProductViewModel> GetAllCategories();
        DataTable AddProduct(ProductViewModel product);
        List<ProductViewModel> GetProductList();
        ProductViewModel GetProductById(int productId);
        byte[] GetProductImageById(int productId);
        DataTable EditProduct(ProductViewModel product);
        bool DeleteProduct(int productId);
    }
}
