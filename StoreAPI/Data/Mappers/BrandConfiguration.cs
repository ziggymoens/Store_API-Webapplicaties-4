using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Data.Mappers
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            //Table name
            builder.ToTable("Brand");
            //Properties
            builder.HasKey(b => b.Id);
            //Associations
            builder.HasMany(b => b.Sneakers)
                .WithOne(s => s.Brand)
                .OnDelete(DeleteBehavior.Restrict);
               
        }
    }
}
