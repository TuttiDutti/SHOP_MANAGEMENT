using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
    }
}
