using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public virtual IList<Author> Authors { get; set; }

    }
}
