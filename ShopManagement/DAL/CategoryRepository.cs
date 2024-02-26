using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class CategoryRepository: BaseRepository<Category>
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
