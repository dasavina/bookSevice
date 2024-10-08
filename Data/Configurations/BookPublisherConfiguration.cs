using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
    {
        public void Configure(EntityTypeBuilder<BookPublisher> builder)
        {
            builder.ToTable("BookPublishers");

            builder.HasKey(bp => new { bp.BookId, bp.PublisherId }); // Composite Key

            // Foreign Key to Book
            builder.HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublishers)
                .HasForeignKey(bp => bp.BookId);

            // Foreign Key to Publisher
            builder.HasOne(bp => bp.Publisher)
                .WithMany(p => p.BookPublishers)
                .HasForeignKey(bp => bp.PublisherId);
        }
    }

}
