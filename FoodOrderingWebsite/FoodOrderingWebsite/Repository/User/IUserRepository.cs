using FoodOrderingWebsite.ViewModel;
using System.Data;

namespace FoodOrderingWebsite.Repository.User
{
    public interface IUserRepository
    {
        public DataTable AddUser(RegisterViewModel register);
    }
}
