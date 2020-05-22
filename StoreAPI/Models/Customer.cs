using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class Customer : IEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //Bids & asks
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Ask> Asks { get; set; }

        public Customer()
        {
            Bids = new List<Bid>();
            Asks = new List<Ask>();
        }
        public Customer(string name, string lastName, string email) : this()
        {
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public void AddBid(Sneaker sneaker, double size, double price)
        {
            Bids.Add(new Bid(this, sneaker, size, price));
        }

        public void AddAsk(Sneaker sneaker, double size, double price)
        {
            Asks.Add(new Ask(this, sneaker, size, price));
        }
    }
}
