using Data.Configurations;
using Data.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetBookWithAuthorAsync(int id);  // Eager Loading
        Task<Book> GetBookExplicitLoadingAsync(int id);  // Explicit Loading
        Task<IEnumerable<Book>> GetBooksWithPublishersAsync();  // Many-to-many loading
        Task<IEnumerable<BookDto>> GetBooksAsync(string titleFilter, string sortBy, int page, int pageSize);
    }
}

    
