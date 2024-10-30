using Data.Configurations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
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
