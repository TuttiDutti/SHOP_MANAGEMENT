using System.ComponentModel;

namespace ShopManagement.ViewModels
{
    public class ProductOrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Firma")]
        public string Company { get; set; }
        [DisplayName("Numer seryjny")]
        public string SerialNumber { get; set; }
        [DisplayName("Cena netto")]
        public decimal NetPrice { get; set; }
		[DisplayName("VAT")]
		public decimal VAT { get; set; }
		[DisplayName("Cena brutto")]
		public decimal GrossPrice { get; set; }
		[DisplayName("Ilość")]
        public int Amuont { get; set; }
        [DisplayName("Razem")]
        public decimal Total { get; set; }
    }
}
