using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "author1", Pseudonym = "pseudonym1", ShortBio = "-" },
                new Author { Id = 2, Name = "author2", Pseudonym = "pseudonym2", ShortBio = "---" }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "publisher1" },
                new Publisher { Id = 2, Name = "publisher2" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "book1", ISBN = "111-1234567890", PublishedDate = new DateTime(20021, 6, 8), AuthorId = 1 },
                new Book { Id = 2, Title = "book2", ISBN = "222-1234567890", PublishedDate = new DateTime(2021, 6, 26), AuthorId = 2 }
            );

            // Seed BookPublishers (Many-to-Many relationships)
            modelBuilder.Entity<BookPublisher>().HasData(
                new BookPublisher { BookId = 1, PublisherId = 1 },
                new BookPublisher { BookId = 2, PublisherId = 2 }
            );
        }
    }

}
