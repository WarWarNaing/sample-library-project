using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Mapping
{
    class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            // Primary Key
            this.HasKey(t => t.AuthorId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Author");
            this.Property(t => t.AuthorId).HasColumnName("AuthorId");
            this.Property(t => t.AuthorName).HasColumnName("AuthorName");
            this.Property(t => t.AccessTime).HasColumnName("AccessTime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
