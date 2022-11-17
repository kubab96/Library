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
        [MaxLength(13, ErrorMessage = "ISBN is too long")]
        [MinLength(13, ErrorMessage = "ISBN is too short")]
        public string ISBN { get; set; }
        //[Range(1,5)]
        //public double Rating { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }

    public class BookDTO : CreateBookDTO
    {
        public int Id { get; set; }
        public IList<AuthorOnlyDTO> Authors { get; set; }
    }

    public class BookOnlyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
    }
}
