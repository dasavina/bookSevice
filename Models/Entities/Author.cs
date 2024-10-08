using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pseudonym { get; set; }
        public string ShortBio { get; set; }
        public ICollection<Book> Books { get; set; }
    }



}
