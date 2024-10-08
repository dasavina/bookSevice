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
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<Publisher> GetPublisherWithBooksAsync(int id); // Eager loading for books
    }

    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Eager loading: includes related Books
        public async Task<Publisher> GetPublisherWithBooksAsync(int id)
        {
            return await _dbSet.Include(p => p.BookPublishers)
                               .ThenInclude(bp => bp.Book)
                               .FirstOrDefaultAsync(p => p.Id == id);
        }
    }

}
