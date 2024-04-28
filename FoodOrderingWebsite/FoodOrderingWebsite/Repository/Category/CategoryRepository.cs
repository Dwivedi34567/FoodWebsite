using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.ViewModel;
using Microsoft.AspNetCore.Http.Extensions;
using System.Data;
using System.Net.Http;
using System.Reflection.Metadata;

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
    }
}
