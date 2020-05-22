using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Data.Repositories
{
    public class BidRepository : IRepository<Bid>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bid> _bids;

        public BidRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _bids = _context.Bids;
        }
        public void Add(Bid entity)
        {
            _bids.Add(entity);
        }

        public void Delete(Bid entity)
        {
            _bids.Remove(entity);
        }

        public Bid FindById(int id)
        {
            return _bids.Include(e => e.Customer).Include(e => e.Sneaker).ThenInclude(s => s.Brand).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Bid> FindByString(string str)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Bid> GetAll()
        {
            return _bids.Include(e => e.Customer).Include(e => e.Sneaker).ThenInclude(s => s.Brand).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGet(int id, out Bid entity)
        {
            entity = _bids.FirstOrDefault(e => e.Id == id);
            return entity != null;
        }

        public void Update(Bid entity)
        {
            _context.Update(entity);
        }
    }
}
