using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class Bid : IEntity
    {
        public string TypeTransaction { get; set; }
        public Customer Customer { get; set; }
        public Sneaker Sneaker { get; set; }
        public double Size { get; set; }
        public double Price { get; set; }

        public Bid()
        {

        }

        public Bid(Customer customer, Sneaker sneaker, double size, double price)
        {
            TypeTransaction = "Bid";
            Price = price;
            Size = size;
            Customer = customer;
            Customer.Bids.Add(this);
            Sneaker = sneaker;
            Sneaker.Bids.Add(this);
        }
    }
}
