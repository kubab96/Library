using Newtonsoft.Json;
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
    }
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<BookOnlyDTO> Books { get; set; }
    }

    public class AuthorOnlyDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    //public class CreateAuthorWithBook
    //{
    //    public IList<BookOnlyDTO> Books { get; set; }
    //}
}
