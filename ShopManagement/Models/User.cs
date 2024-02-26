using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$"
            , ErrorMessage = "Hasło musi mieć minimum 8 znaków, zawierać conajmniej 1 cyfrę, conajmniej 1 małą literę, conajmniej 1 dużą literę oraz znak specjalny")]
        public string Password { get; set; }

        [DisplayName("Numer telefonu")]
        [StringLength(20)]
        public string Number { get; set; }

        [Required(ErrorMessage = "Dni urlopowe są wymagane")]
        [DisplayName("Dni urlopowe")]
        public int VacationDays { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DisplayName("Ostatnie logowanie")]
        public DateTime LogTime { get; set; }

        [DisplayName("Rola")]
        
        public int RoleId { get; set; }
        [DisplayName("Rola")]
        public Role Role { get; set; }

        public ICollection<Absence> Absence { get; } = new List<Absence>();

        public ICollection<Document> Documents { get; } = new List<Document>();
        [Required]
        [DisplayName("Blokada")]
        public bool isBlocked { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();

        public ICollection<Announcement> Announcements { get; } = new List<Announcement>();

    }
}
