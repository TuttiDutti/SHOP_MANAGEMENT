using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class UserRepository : BaseRepository<User>
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
