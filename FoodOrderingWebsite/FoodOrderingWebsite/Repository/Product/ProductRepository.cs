using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.Product
{
    public class ProductRepository:IProductRepository
    {
        private readonly DbHelper _dbHelper;

        /// <summary>
        /// Constructor for CategoryRepository class
        /// </summary>
        /// <param name="dbHelper"></param>
        public ProductRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }
        public List<ProductViewModel> GetAllCategories()
        {
            try
            {
                string procedureName = "spGetProductCategory";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                List<ProductViewModel> CategoryList = new List<ProductViewModel>();
                foreach (DataRow row in result.Rows)
                {
                    ProductViewModel Category = new ProductViewModel();
                    Category.CategoryID = Convert.ToInt32(row["CategoryID"]);
                    Category.ProductCategory = row["Name"].ToString();

                    CategoryList.Add(Category);
                }
                return CategoryList;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.Write(ex.ToString());
                return new List<ProductViewModel>();
            }
        }

        public DataTable AddProduct(ProductViewModel product)
        {
            try
            {
                string procedureName = "spAddProduct";

                // Convert the product image data to base64 string
               

                // Prepare the parameters for the stored procedure
                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "ProductName", product.ProductName },
            { "ProductDescription", product.ProductDescription },
            { "Price", product.ProductPrice },
            { "ImageUrl", product.ProductImage }, // Use the base64 string here
            { "CategoryID", product.CategoryID },
            { "IsActive", product.IsActive }
        };

                // Execute the stored procedure
                return _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
            }
            catch
            {
                throw;
            }
        }

        public List<ProductViewModel> GetProductList()
        {
            try
            {
                string procedureName = "spGetProduct";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                List<ProductViewModel> ProductList = new List<ProductViewModel>();
                foreach (DataRow row in result.Rows)
                {
                    ProductViewModel Product = new ProductViewModel();
                    Product.ProductId = Convert.ToInt32(row["ProductID"]);
                    Product.ProductName = row["Name"].ToString();
                    Product.ProductImage = (byte[])row["ImageUrl"];
                    // Convert "IsActive" to bool
                    Product.ProductPrice = Convert.ToDecimal(row["Price"]);

                    Product.IsActive = Convert.ToBoolean(row["IsActive"]);
                    Product.ProductCategory = row["CategoryName"].ToString();

                    ProductList.Add(Product);
                }
                return ProductList;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.Write(ex.ToString());
                return new List<ProductViewModel>();
            }
        }

        public ProductViewModel GetProductById(int productId)
        {
            try
            {
                ProductViewModel product = new ProductViewModel();
                string procedureName = "spGetProductByID";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                 {
                       { "ProductId", productId }
                 };
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                if (result.Rows.Count > 0)
                {
                    // Assuming categoryId is in the first row and first column of the DataTable
                    // You might need to adjust this based on your actual DataTable structure
                    product.ProductId = Convert.ToInt32(result.Rows[0]["ProductId"]);
                    product.CategoryID = Convert.ToInt32(result.Rows[0]["CategoryId"]);
                    product.ProductPrice = Convert.ToDecimal(result.Rows[0]["Price"]);
                    product.ProductName = result.Rows[0]["Name"].ToString();
                    product.ProductDescription = result.Rows[0]["Description"].ToString();
                    // Assuming Name, IsActive, and ImageUrl are in columns with these names
                    // You might need to adjust these based on your actual DataTable structure
                    product.ProductCategory = result.Rows[0]["CategoryName"].ToString();
                    product.IsActive = Convert.ToBoolean(result.Rows[0]["IsActive"]);
                    product.ProductImage = (byte[])result.Rows[0]["ImageUrl"];
                }
                return product;

            }
            catch
            {
                throw;
            }
        }

        public byte[] GetProductImageById(int productId)
        {
            try
            {
                byte[] imageUrl = null;
                string procedureName = "spGetProductImageByID";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                 {
                       { "ProductId", productId }
                 };
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                if (result.Rows.Count > 0)
                {
                    imageUrl = (byte[])result.Rows[0]["ImageUrl"];
                }
                return imageUrl;
            }
            catch
            {
                throw;
            }
        }

        public DataTable EditProduct(ProductViewModel product)
        {
            try
            {
                string procedureName = "spEditProduct";
               // string base64ImageData = category.ImageData != null ? Convert.ToBase64String(category.ImageData) : null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                 {
                       { "ProductId", product.ProductId },
                       { "ProductName", product.ProductName },
                       { "ImageUrl", product.ProductImage},
                       { "IsActive", product.IsActive},
                       { "CategoryId", product.CategoryID },
                       { "ProductDescription", product.ProductDescription},
                       { "Price", product.ProductPrice}
                 };
                return _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteProduct(int productId)
        {
            try
            {
                bool status = false;
                string procedureName = "spDeleteProduct";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                 {
                       { "ProductId", productId }
                 };
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                status = Convert.ToBoolean(result.Rows[0]["Status"]);
                return status;
            }
            catch
            {
                throw;
            }
        }
    }
}
