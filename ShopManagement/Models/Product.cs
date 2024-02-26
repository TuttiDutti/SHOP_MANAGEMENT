using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Firma")]
        public string Company { get; set; }
        [DisplayName("Numer seryjny")]
        public string SerialNumber { get; set; }
        [Required]
        [DisplayName("Kod kreskowy")]
        public string BarCode { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Cena netto")]
        public decimal NetPrice { get; set; }
        [Required]
        [DisplayName("Cena brutto")]
        public decimal GrossPrice { get; set; }
        [Required]
        [DisplayName("VAT")]
        public int VAT { get; set; }
        [DisplayName("Promocja")]
        public int Offer { get; set; }
        [Required]
        [DisplayName("Ilość")]
        public int Amount { get; set; }
        [DisplayName("Zdjęcia")]
        public ICollection<Photo> Photos { get; } = new List<Photo>();

        [Required]
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
        [DisplayName("Kategoria")]
        public Category Category { get; set; }
        [DisplayName("Blokada")]
        public bool IsBlocked { get; set; }

        public List<OrderItem> OrderItems { get; } = new();

    }
}
