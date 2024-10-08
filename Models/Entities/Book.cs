﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookPublisher> BookPublishers { get; set; }
    }

}