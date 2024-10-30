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

   
}
