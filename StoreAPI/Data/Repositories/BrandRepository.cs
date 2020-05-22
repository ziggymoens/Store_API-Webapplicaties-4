using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StoreAPI.Data.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Brand> _brands;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _brands = _context.Brands;
        }

        public void Add(Brand entity)
        {
            _brands.Add(entity);
        }

        public void Delete(Brand entity)
        {
            _brands.Remove(entity);
        }

        public Brand FindById(int id)
        {
            return _brands.Include(b => b.Sneakers).ThenInclude(s => s.Stock).FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Brand> FindByString(string str)
        {
            return _brands.Include(b => b.Sneakers).ThenInclude(s => s.Stock).Where(b => b.Name == str);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brands.Include(b => b.Sneakers).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGet(int id, out Brand entity)
        {
            entity = _brands.FirstOrDefault(b => b.Id == id);
            return entity != null;
        }

        public void Update(Brand entity)
        {
            _context.Update(entity);
        }
    }
}
