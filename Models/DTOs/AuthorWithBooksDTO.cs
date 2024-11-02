using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class AuthorWithBooksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pseudonym { get; set; }
        public string ShortBio { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
