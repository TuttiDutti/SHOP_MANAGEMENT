using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Zdjęcie")]
        public byte[] Content { get; set; }
        [Required]
        [DisplayName("Miniaturka")]
        public bool IsMain { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
