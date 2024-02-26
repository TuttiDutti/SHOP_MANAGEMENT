using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class AbsenceRepository : BaseRepository<Absence>
    {
        private ApplicationDbContext _context;
        public AbsenceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
