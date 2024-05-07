using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.Utility;
using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.User
{
    public class UserRepository:IUserRepository
    {
        private readonly DbHelper _dbHelper;

        /// <summary>
        /// Constructor for CategoryRepository class
        /// </summary>
        /// <param name="dbHelper"></param>
        public UserRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }
        public DataTable AddUser(RegisterViewModel register)
        {
            try
            {
                string procedureName = "spAddUser";

                // Hash the password
                string hashedPassword = Crypt.ComputeMD5Hash(register.ConfirmPassword);

                // Prepare the parameters for the stored procedure
                Dictionary<string, object> parameters = new Dictionary<string, object>
                  {
                     { "Username", register.UserName },
                     { "Email", register.Email },
                     { "Password", hashedPassword }
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

