using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Data.Repositories
{
    public class AskRepository : IRepository<Ask>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Ask> _asks;

        public AskRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _asks = _context.Asks;
        }

        public void Add(Ask entity)
        {
            _asks.Add(entity);
        }

        public void Delete(Ask entity)
        {
            _asks.Remove(entity);
        }

        public Ask FindById(int id)
        {
            return _asks.Include(e => e.Customer).Include(e => e.Sneaker).ThenInclude(s => s.Brand).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Ask> FindByString(string str)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Ask> GetAll()
        {
            return _asks.Include(e => e.Customer).Include(e => e.Sneaker).ThenInclude(s => s.Brand).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGet(int id, out Ask entity)
        {
            entity = _asks.FirstOrDefault(a => a.Id == id);
            return entity != null;
        }

        public void Update(Ask entity)
        {
            _context.Update(entity);
        }
    }
}
