using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StoreAPI.Models;

namespace StoreAPI.Data.Repositories
{
    public class SneakerRepository : IRepository<Sneaker>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Sneaker> _sneakers;

        public SneakerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _sneakers = _context.Sneakers;
        }

        public void Add(Sneaker entity)
        {
            _sneakers.Add(entity);
        }

        public void Delete(Sneaker entity)
        {
            _sneakers.Remove(entity);
        }

        public Sneaker FindById(int id)
        {
            return _sneakers.Include(s => s.Brand).Include(s => s.Stock).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Sneaker> FindByString(string name)
        {
            return _sneakers.Include(s => s.Brand).Include(s => s.Stock).Where(s => s.Name == name).ToList();
        }

        public IEnumerable<Sneaker> GetAll()
        {
            return _sneakers.Include(s => s.Brand).Include(s => s.Stock).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGet(int id, out Sneaker entity)
        {
            entity = _sneakers.FirstOrDefault(s => s.Id == id);
            return entity != null;
        }

        public void Update(Sneaker entity)
        {
            _context.Update(entity);
        }
    }
}
