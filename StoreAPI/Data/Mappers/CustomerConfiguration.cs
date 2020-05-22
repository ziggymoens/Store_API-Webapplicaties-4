using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Data.Mappers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Table name
            builder.ToTable("Customer");
            //Properties
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            //Associations
            builder.HasMany(c => c.Asks).WithOne(a => a.Customer).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Bids).WithOne(b => b.Customer).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
