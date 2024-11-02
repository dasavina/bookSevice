using Data.Configurations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookPublisherRepository : GenericRepository<BookPublisher>, IBookPublisherRepository
    {

        public BookPublisherRepository(ApplicationDbContext context):base(context)
        {
        }

        public async Task AddPublisherToBookAsync(int bookId, int publisherId)
        {
            var bookPublisher = new BookPublisher { BookId = bookId, PublisherId = publisherId };
            await _context.BookPublishers.AddAsync(bookPublisher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publisher>> GetPublishersByBookIdAsync(int bookId)
        {
            return await _context.BookPublishers
                .Where(bp => bp.BookId == bookId)
                .Select(bp => bp.Publisher)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByPublisherIdAsync(int publisherId)
        {
            return await _context.BookPublishers
                .Where(bp => bp.PublisherId == publisherId)
                .Select(bp => bp.Book)
                .ToListAsync();
        }

    }

}
