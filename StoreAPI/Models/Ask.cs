using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class Ask : IEntity
    {
        public string TypeTransaction { get; set; }
        public Customer Customer { get; set; }
        public Sneaker Sneaker { get; set; }
        public double Size { get; set; }
        public double Price { get; set; }
        public bool Sold { get; set; }

        public Ask()
        {

        }

        public Ask(Customer customer, Sneaker sneaker, double size, double price)
        {
            TypeTransaction = "Ask";
            Price = price;
            Size = size;
            Customer = customer;
            Customer.Asks.Add(this);
            Sneaker = sneaker;
            Sneaker.Asks.Add(this);
            Sneaker.AddStock(size, 1);
        }
    }
}
