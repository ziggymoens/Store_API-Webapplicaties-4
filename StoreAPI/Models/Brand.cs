using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class Brand : IEntity
    {
        public string Name { get; set; }
        public ICollection<Sneaker> Sneakers { get; set; }
        public Brand()
        {
            Sneakers = new List<Sneaker>();
        }
        public Brand(string name):this()
        {
            Name = name;
        }

        public void AddSneaker(Sneaker sneaker)
        {
            sneaker.Brand = this;
            Sneakers.Add(sneaker);
        }

        public Sneaker GetSneaker(int id) => Sneakers.FirstOrDefault(s => s.Id == id);
    }
}
