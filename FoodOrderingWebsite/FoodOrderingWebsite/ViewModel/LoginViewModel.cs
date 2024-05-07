using System.ComponentModel.DataAnnotations;

namespace FoodOrderingWebsite.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter the valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        //[RegularExpression("(?=^.{8,}$)((?=.\\d)|(?=.\\W+))(?![.\\n])(?=.[A-Z])(?=.[a-z]).*$", ErrorMessage = "Password should have at least one lowercase letter," + " one uppercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}
