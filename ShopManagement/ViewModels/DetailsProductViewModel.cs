using ShopManagement.Models;

namespace ShopManagement.ViewModels
{
    public class DetailsProductViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Company { get; set; }

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }

        public string Description { get; set; }


        public decimal NetPrice { get; set; }

        public decimal GrossPrice { get; set; }

        public int VAT { get; set; }
        public int Offer { get; set; }

        public int Amount { get; set; }

        public string Category { get; set; }

        public bool IsBlocked { get; set; }
    }
}
