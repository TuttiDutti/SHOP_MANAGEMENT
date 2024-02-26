using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class AnnouncementRepositiory : BaseRepository<Announcement>
    {
        private ApplicationDbContext _context;
        public AnnouncementRepositiory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
