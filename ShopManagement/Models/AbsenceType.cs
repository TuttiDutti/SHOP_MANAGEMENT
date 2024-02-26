using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class AbsenceType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Absence> Absence { get; } = new List<Absence>();
    }
}
