using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private ApplicationDbContext _context;
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
