using Data.Configurations;
using Data.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
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
    }

    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Eager loading: includes related Author
        public async Task<Book> GetBookWithAuthorAsync(int id)
        {
            return await _dbSet.Include(b => b.Author)
                               .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Explicit loading
        public async Task<Book> GetBookExplicitLoadingAsync(int id)
        {
            var book = await _dbSet.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                await _context.Entry(book).Reference(b => b.Author).LoadAsync();
            }
            return book;
        }

        // Many-to-many relationship
        public async Task<IEnumerable<Book>> GetBooksWithPublishersAsync()
        {
            return await _dbSet.Include(b => b.BookPublishers)
                               .ThenInclude(bp => bp.Publisher)
                               .ToListAsync();
        }
    }

}
