using ShopManagement.Models;

namespace ShopManagement.DAL
{
    public class DocumentTypeRepository : BaseRepository<DocumentType>
    {
        private ApplicationDbContext _context;
        public DocumentTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
