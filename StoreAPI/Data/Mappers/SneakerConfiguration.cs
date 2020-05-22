using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Data.Mappers
{
    public class SneakerConfiguration : IEntityTypeConfiguration<Sneaker>
    {
        public void Configure(EntityTypeBuilder<Sneaker> builder)
        {
            //Table name
            builder.ToTable("Sneaker");
            //Properties
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Color).HasMaxLength(100);
            //Associations
            builder.HasMany(s => s.Bids).WithOne(b => b.Sneaker).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(s => s.Asks).WithOne(a => a.Sneaker).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
