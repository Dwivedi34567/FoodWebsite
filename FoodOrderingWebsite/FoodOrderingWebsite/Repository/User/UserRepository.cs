using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.ViewModel;
using Microsoft.Win32;
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
        public DataTable Register(RegisterViewModel register)
        {
            try
            {
                string procedureName = "spAddUser";

                // Hash the password
                string hashedPassword = Crypt.Encrypt(register.ConfirmPassword);

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
        public RegisterViewModel LoginUser(LoginViewModel login)
        {
            try
            {
                string procedureName = "spLoginUser";
                string hashedPassword = Crypt.Encrypt(login.Password); ;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "Email", login.Email },
                    { "Password", hashedPassword }
                };
                DataTable userData = _dbHelper.ExecuteStoredProcedure(procedureName, parameters);
                RegisterViewModel result = new RegisterViewModel();
                if(userData != null)
                {
                   result.Password = Crypt.Decrypt(userData.Rows[0]["Password"].ToString());
                   result.Email = userData.Rows[0]["Email"].ToString();
                   result.UserName = userData.Rows[0]["Email"].ToString();
                }
                return result;
            }
            catch
            {
                throw;
            }
        }


    }
}

