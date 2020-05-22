using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StoreAPI.Models;

namespace StoreAPI.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationDbContext _context;
    private readonly DbSet<Customer> _customers;

        public CustomerRepository(ApplicationDbContext dbContex)
        {
            _context = dbContex;
            _customers = _context.Customers;
        }

        public void Add(Customer entity)
        {
            _customers.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _customers.Remove(entity);
        }

        public Customer FindById(int id)
        {
           return _customers.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> FindByString(string email)
        {
            return _customers.Where(c => c.Email == email);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGet(int id, out Customer entity)
        {
            entity = _customers.FirstOrDefault(c => c.Id == id);
            return entity != null;
        }

        public void Update(Customer entity)
        {
            _context.Update(entity);
        }
    }
}
