using ShopManagement.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj firmę")]
        [DisplayName("Firma")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Podaj numer seryjny")]
        [DisplayName("Numer seryjny")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "Podaj kod kreskowy")]
        [DisplayName("Kod kreskowy")]
        public string BarCode { get; set; }
        [Required(ErrorMessage = "Podaj cenę netto")]
        [DisplayName("Cena netto")]
        public decimal NetPrice { get; set; }
        [DisplayName("Cena brutto")]
        public decimal GrossPrice { get; set; }
        [DisplayName("VAT")]
        public int VAT { get; set; }
        [DisplayName("Promocja")]
        public int Offer { get; set; }
        [DisplayName("Ilość")]
        public int Amount { get; set; }
        [DisplayName("Kategoria")]
        public string Category { get; set; }
        public byte[] MainPhoto { get; set; }
        [DisplayName("Blokada")]
        public bool IsBlocked { get; set; }
    }
}
