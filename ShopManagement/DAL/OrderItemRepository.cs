using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class OrderItemRepository : BaseRepository<OrderItem>
    {
        private ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
