using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Location");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.StateDivision).HasColumnName("StateDivision");
            this.Property(t => t.Township).HasColumnName("Township");
            this.Property(t => t.AccessTime).HasColumnName("AccessTime");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
