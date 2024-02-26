using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ShopManagement.Models;

namespace ShopManagement.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Imię")]
        [Required(ErrorMessage = "Imie jest wymagane")]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "Login jest wymagany")]
        [StringLength(25)]
        public string Login { get; set; }

        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&_?]).{8,}$"
            , ErrorMessage = "Hasło musi mieć minimum 8 znaków, zawierać conajmniej 1 cyfrę, conajmniej 1 małą literę, conajmniej 1 dużą literę oraz znak specjalny")]
        public string Password { get; set; }

        [DisplayName("Numer telefonu")]
        [StringLength(20)]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        public string Number { get; set; }
        [DisplayName("Dni urlopowe")]
        [Required(ErrorMessage = "Dni urlopowe są wymagane")]
        public int VacationDays { get; set; }
        [DisplayName("Rola")]
        public int RoleId { get; set; }
    }
}
