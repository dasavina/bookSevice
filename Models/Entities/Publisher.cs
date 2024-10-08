using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookPublisher> BookPublishers { get; set; }
    }

}
