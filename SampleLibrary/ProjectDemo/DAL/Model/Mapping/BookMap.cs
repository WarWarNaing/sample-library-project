using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Mapping
{
   public   class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.BookId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Book");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.BookName).HasColumnName("BookName");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");
            this.Property(t => t.AuthorName).HasColumnName("AuthorName");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.BookPrice).HasColumnName("BookPrice");
            this.Property(t => t.RentPrice).HasColumnName("RentPrice");
            this.Property(t => t.Photo).HasColumnName("Photo");
            this.Property(t => t.ReachDate).HasColumnName("ReachDate");
            this.Property(t => t.IsAvailable).HasColumnName("IsAvailable");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
