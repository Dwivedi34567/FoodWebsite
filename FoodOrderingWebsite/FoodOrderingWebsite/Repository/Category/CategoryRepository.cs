using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.ViewModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Data;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;

namespace FoodOrderingWebsite.Repository.Category
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly DbHelper _dbHelper;

        /// <summary>
        /// Constructor for CategoryRepository class
        /// </summary>
        /// <param name="dbHelper"></param>
        public CategoryRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public DataTable AddCategory(CategoryViewModel category)
        {
            try
            {
                string procedureName = "spAddCategory";
                string base64ImageData = category.ImageData != null ? Convert.ToBase64String(category.ImageData) : null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                 {

                       { "CategoryName",category.CategoryName },
                       {"ImageUrl",base64ImageData},
                       { "IsActive",category.IsActive}
                 };
                return _dbHelper.ExecuteStoredProcedure(procedureName, parameters);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Me
        /// </summary>
        /// <returns></returns>
        public List<CategoryViewModel> GetCategoryList()
        {
            try
            {
                string procedureName = "spGetCategory";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable result = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                List<CategoryViewModel> CategoryList = new List<CategoryViewModel>();
                foreach (DataRow row in result.Rows)
                {
                    CategoryViewModel Category = new CategoryViewModel();
                    Category.CategoryID = Convert.ToInt32(row["CategoryID"]);
                    Category.CategoryName = row["Name"].ToString();

                    // Retrieve image data as byte array
                    if (row["ImageUrl"] != DBNull.Value)
                    {
                        Category.ImageData = (byte[])row["ImageUrl"];
                    }

                    // Convert "IsActive" to bool
                    Category.IsActive = Convert.ToBoolean(row["IsActive"]);

                    CategoryList.Add(Category);
                }
                return CategoryList;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error occurred while getting category list.", ex);
            }
        }


    }
}
