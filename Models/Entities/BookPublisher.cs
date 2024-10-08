using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class BookPublisher
    {
        public int BookId { get; set; } // Foreign Key to Book
        public Book Book { get; set; }

        public int PublisherId { get; set; } // Foreign Key to Publisher
        public Publisher Publisher { get; set; }
    }

}
