using FoodOrderingWebsite.Helper;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;

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
    }
}
