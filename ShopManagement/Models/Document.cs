using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa pliku")]
        public string FileName { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        [DisplayName("Tylko admin")]
        public bool IsPrivate { get; set; }
        [Required]
        [DisplayName("Data wgrania")]
        public DateTime UploadDate { get; set; }
        [Required]
        [DisplayName("Użytkownik")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [DisplayName("Rodzaj")]
        public int DocumentTypeId { get; set; }
        [DisplayName("Rodzaj")]
        public DocumentType DocumentType { get; set; }
    }
}
