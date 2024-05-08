using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.ViewModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace FoodOrderingWebsite.Repository.User
{
    public class UserRepository:IUserRepository
    {
        private readonly DbHelper _dbHelper;
        private IConfiguration _config;

        /// <summary>
        /// Constructor for CategoryRepository class
        /// </summary>
        /// <param name="dbHelper"></param>

        public UserRepository(DbHelper dbHelper, IConfiguration config)
        {
            _dbHelper = dbHelper;
            _config = config;
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

        public string GenerateToken(LoginViewModel user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email)
                };

                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(1), 
                    signingCredentials: credentials
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtToken;

            }
            catch
            {
                throw;
            }
        }

    }
}

