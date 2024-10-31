using Data.Configurations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
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

        public async Task<IEnumerable<Book>> GetBooksAsync(string titleFilter = null, string sortBy = null, int page = 1, int pageSize = 10)
        {
            var books = _context.Books.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(titleFilter))
            {
                books = books.Where(b => b.Title.Contains(titleFilter));
            }

            // Sorting
            books = sortBy switch
            {
                "title" => books.OrderBy(b => b.Title),
                "publishedDate" => books.OrderBy(b => b.PublishedDate),
                _ => books.OrderBy(b => b.Id) // Default sorting by Id
            };

            // Paging
            books = books.Skip((page - 1) * pageSize).Take(pageSize);

            return await books.ToListAsync();
        }

    }

}
