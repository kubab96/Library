using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class CreateAuthorDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }
        [Required]
        public int BookId { get; set; }
    }
    public class AuthorDTO : CreateAuthorDTO
    {
        public int Id { get; set; }
        public IList<BookDTO> Books { get; set; }

    }
}
