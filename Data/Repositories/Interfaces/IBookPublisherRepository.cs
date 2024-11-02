using Data.Repositories.Repositories;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IBookPublisherRepository: IGenericRepository<BookPublisher>
    {
        Task AddPublisherToBookAsync(int bookId, int publisherId);
        Task<IEnumerable<Publisher>> GetPublishersByBookIdAsync(int bookId);
        Task<IEnumerable<Book>> GetBooksByPublisherIdAsync(int publisherId);

    }
}
