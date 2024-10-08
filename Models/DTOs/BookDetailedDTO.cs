using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class BookDetailedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public AuthorDto Author { get; set; }
        public List<PublisherDto> Publishers { get; set; }  // Many-to-Many relation
    }

}
