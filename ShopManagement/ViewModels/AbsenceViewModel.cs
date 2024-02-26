using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ViewModels
{
    public class AbsenceViewModel
    {
        [Required(ErrorMessage = "Podaj datę")]
        [DisplayName("Data od")]
        public DateTime DateFrom { get; set; }
        [Required(ErrorMessage = "Podaj datę")]
        [DisplayName("Data do")]
        public DateTime DateTo { get; set; }
        [Required(ErrorMessage = "Podaj rodzaj nieobecności")]
        [DisplayName("Rodzaj nieobecności")]
        public int AbsenceTypeId { get; set; }

    }
}
