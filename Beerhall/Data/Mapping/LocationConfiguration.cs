using Beerhall.Models;
using Beerhall.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Data.Mapping
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            //Table name
            builder.ToTable("Location");
            //Primary Key
            builder.HasKey(l => l.PostalCode);
            //Properties
            builder.Property(l => l.PostalCode)
                .HasMaxLength(4);
            builder.Property(l => l.Name)
                 .IsRequired()
                 .HasMaxLength(50);
        }
    }
}
