using Library.Data;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class CreateBookDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public IList<CreateAuthorDTO> Authors { get; set; }
    }

    public class UpdateBookDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        public IList<CreateAuthorDTO> Authors { get; set; }
    }

    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public IList<AuthorOnlyDTO> Authors { get; set; }
    }

    public class BookOnlyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
    }
}
