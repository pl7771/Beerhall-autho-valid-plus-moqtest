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
    public class BeerConfiguration : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            //table name
            builder.ToTable("Beer");
            // Properties
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
        }
    }
}
