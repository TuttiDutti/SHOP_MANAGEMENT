using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ViewModels
{
    public class NewProductViewModel
    {       
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
        [Required(ErrorMessage = "Podaj opis")]
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Podaj cenę netto")]
        [DisplayName("Cena netto")]
        [RegularExpression(@"^[0-9,]+$")]
        public decimal NetPrice { get; set; }
        [DisplayName("Cena brutto")]
        public decimal GrossPrice { get; set; }
        [Required(ErrorMessage = "Podaj VAT")]
        [DisplayName("VAT")]
        public int VAT { get; set; }
        [Required(ErrorMessage = "Podaj ilość")]
        [DisplayName("Ilość")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Podaj kategorię")]
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
    }
}
