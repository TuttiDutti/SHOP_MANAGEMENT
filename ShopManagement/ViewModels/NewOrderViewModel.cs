using MessagePack;
using ShopManagement.Models;

namespace ShopManagement.ViewModels
{
    public class NewOrderViewModel
    {
        public int ProductId { get; set; }        
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        
    }
}
