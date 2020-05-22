using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class Sneaker : IEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public Brand Brand { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Stock> Stock { get; set; }
        public string Barcode { get; set; }
        public string onlineImg { get; set; }


        //Bids & asks
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Ask> Asks { get; set; }

        public Sneaker()
        {
            Stock = new List<Stock>();
            Bids = new List<Bid>();
            Asks = new List<Ask>();
        }
        public Sneaker(string name, string color, double price, DateTime releaseDate, Brand brand = null) : this()
        {
            Name = name;
            Price = price;
            Color = color;
            Brand = brand;
            ReleaseDate = releaseDate;
        }

        public void AddStock(double size, int amount)
        {
            Stock stock = Stock.FirstOrDefault(s => s.Size == size);
            if (stock != null)
            {
                stock.AddStock(amount);
            }
            else { Stock.Add(new Stock(size, amount)); }
        }

        public int GetStock(double size)
        {
            Stock stock = Stock.FirstOrDefault(s => s.Size == size);
            return stock != null ? stock.Amount : 0;
        }

        public void AddBrand(Brand brand)
        {
            brand.AddSneaker(this);
            Brand = brand;
        }

        public void AddBarcode(string barcode)
        {
            this.Barcode = barcode;
        }
    }
}
