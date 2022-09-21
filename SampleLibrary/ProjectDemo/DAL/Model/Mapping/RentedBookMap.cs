using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    class RentedBookMap : EntityTypeConfiguration<RentedBook>
    {
        public RentedBookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RentedBook");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.BookName).HasColumnName("BookName");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.StartDate).HasColumnName("StartDate");           
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.RentPrice).HasColumnName("RentPrice");
            this.Property(t => t.PenaltyFee).HasColumnName("PenaltyFee");
            this.Property(t => t.IsDeStrict).HasColumnName("IsDeStrict");
            this.Property(t => t.IsRePay).HasColumnName("IsRePay");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.AccessTime).HasColumnName("AccessTime");
        }
    }
}
