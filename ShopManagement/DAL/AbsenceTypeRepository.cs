using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class AbsenceTypeRepository : BaseRepository<AbsenceType>
    {
        private ApplicationDbContext _context;
        public AbsenceTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
