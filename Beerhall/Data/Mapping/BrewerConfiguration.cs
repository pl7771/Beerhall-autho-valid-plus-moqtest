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
    public class BrewerConfiguration : IEntityTypeConfiguration<Brewer>
    {
        public void Configure(EntityTypeBuilder<Brewer> builder)
        {
            //Table name
            builder.ToTable("Brewer");
            //Primary Key
            builder.HasKey(b => b.BrewerId);
            //Properties
            builder.Property(b => b.Name)
                .HasColumnName("BrewerName")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.ContactEmail)
               .HasMaxLength(100);
            builder.Property(b => b.Street)
               .HasMaxLength(100);
            //Associations
            builder.HasMany(b => b.Beers)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Location)
              .WithMany()
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
