using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Data.Mappers;
using StoreAPI.Models;

namespace StoreAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Ask> Asks { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sneaker> Sneakers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new SneakerConfiguration());
        }
    }
}
