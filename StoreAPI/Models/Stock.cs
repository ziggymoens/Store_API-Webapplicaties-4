using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StoreAPI.Models
{
    public class Stock : IEntity
    {
        public int Amount { get; set; }
        public double Size { get; set; }

        public Stock(double size, int amount)
        {
            Size = size;
            Amount = amount;
        }

        internal void AddStock(int amount)
        {
            Amount += amount;
        }
    }
}
