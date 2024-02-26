using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        public ICollection<Document> Documents = new List<Document>();
    }
}
