using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Numer")]
        public string Number { get; set; }
        [DisplayName("Uzytkownik")]
        public string User { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Firma")]
        public string Company { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [DisplayName("NIP")]
        public string NIP { get; set; }
        [DisplayName("Rodzaj dokumentu")]
        public string Type { get; set; }

        [DisplayName("Produkty")]
        public List<ProductOrderViewModel> ProductOrders { get; set; }

    }
}
