using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Treść")]
        public string Description { get; set; }
        [Required]
        public DateTime Added { get; set; }
        [Required]
        [DisplayName("Data do")]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
