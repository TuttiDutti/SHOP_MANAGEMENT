using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ShopManagement.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Numer")]
        public string Number { get; set; }
        [Required]
        [DisplayName("Użytkownik")]
        public int UserId { get; set; }
        [DisplayName("Użytkownik")]
        public User User { get; set; }
        [Required]
        [DisplayName("Data utworzenia")]
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Firma")]
        public string Company { get; set; }
        [Required]
        [DisplayName("NIP")]
        public string NIP { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Typ")]
        public string Type { get; set; }
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
