using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.User
{
    public interface IUserRepository
    {
        public DataTable Register(RegisterViewModel register);

        RegisterViewModel LoginUser(LoginViewModel login);
    }
}
