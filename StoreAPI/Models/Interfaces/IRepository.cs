using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int id);
        public bool TryGet(int id, out T entity);
        public void SaveChanges();
        IEnumerable<T> FindByString(string str);
    }
}
