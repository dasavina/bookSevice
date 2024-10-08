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
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> GetAuthorWithBooksAsync(int id); // Eager loading for books
    }

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
