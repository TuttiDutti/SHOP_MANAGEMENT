using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class ProductRepository : BaseRepository<Product>
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
