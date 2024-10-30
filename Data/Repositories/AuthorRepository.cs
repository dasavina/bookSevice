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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Eager loading: includes related Books
        public async Task<Author> GetAuthorWithBooksAsync(int id)
        {
            return await _dbSet.Include(a => a.Books)
                               .FirstOrDefaultAsync(a => a.Id == id);
        }
    }

}
