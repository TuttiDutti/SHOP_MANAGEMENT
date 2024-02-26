using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Absence
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Data od")]
        public DateTime DateFrom { get; set; }
        [Required]
        [DisplayName("Data do")]
        public DateTime DateTo { get; set; }
        [Required]
        public int UserId { get; set; }
        [DisplayName("Użytkownik")]
        public User User { get; set; }
        [Required]
        [DisplayName("Rodzaj")]
        public int AbsenceTypeId { get; set; }
        [DisplayName("Rodzaj")]
        public AbsenceType AbsenceType { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
