using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}
