using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class OrderRepository : BaseRepository<Order>
    {
        private ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
