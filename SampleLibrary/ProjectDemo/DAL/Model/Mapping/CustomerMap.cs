using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.JoinedDate).HasColumnName("JoinedDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
