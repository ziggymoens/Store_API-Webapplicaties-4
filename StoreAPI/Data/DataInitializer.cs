using Microsoft.AspNetCore.Identity;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsers();
                Customer customer1 = new Customer("Ziggy", "Moens", "ziggymoens@hotmail.com");
                Customer customer2 = new Customer("Jan", "Jansens", "jan@hotmail.com");
                Customer customer3 = new Customer("Mark", "Appels", "mark@hotmail.com");
                Customer[] customers = new Customer[] { customer1, customer2, customer3 };
                _dbContext.AddRange(customers);


                Brand nike = new Brand("Nike");
                Brand adidas = new Brand("Adidas");
                Brand supreme = new Brand("Supreme");
                Brand offWhite = new Brand("Off-White");
                Brand airJordan = new Brand("Air Jordan");
                Brand converse = new Brand("Converse");
                Brand babe = new Brand("Bape");
                Brand[] brands = new Brand[] { airJordan, nike, adidas, supreme, offWhite, converse, babe };
                _dbContext.AddRange(brands);

                Sneaker yeezy350Zebra = new Sneaker("Yeezy 350 V2", "Zebra", 220, new DateTime(2017, 02, 25));
                yeezy350Zebra.AddBarcode("CP9654");
                yeezy350Zebra.onlineImg = "Adidas-Yeezy-Boost-350-V2-Zebra";
                Sneaker jordan1Purple = new Sneaker("Jordan 1 Retro High Court", "Purple & White", 170.00, new DateTime(2020, 04, 11));
                jordan1Purple.AddBarcode("555088-500");
                jordan1Purple.onlineImg = "Air-Jordan-1-Retro-High-Court-Purple-White";
                Sneaker supremeAF1White = new Sneaker("Supreme x Nike Air Force 1 Low","White", 96, new DateTime(2020, 03, 05));
                supremeAF1White.AddBarcode("CU9225-100");
                supremeAF1White.onlineImg = "Nike-Air-Force-1-Low-Supreme-Box-Logo-White";
                Sneaker supremeAF1Black = new Sneaker("Supreme x Nike Air Force 1 Low", "Black", 96, new DateTime(2020, 03, 05));
                supremeAF1Black.AddBarcode("CU9225-001");
                supremeAF1Black.onlineImg = "Nike-Air-Force-1-Low-Supreme-Box-Logo-Black";
                Sneaker yeezySlideBone = new Sneaker("Yeezy Slide", "Bone", 55, new DateTime(2019, 12, 6));
                yeezySlideBone.AddBarcode("FW6345");
                yeezySlideBone.onlineImg = "Yeezy-Slide-Bone";
                Sneaker yeezy350Black = new Sneaker("Yeezy 350 V2", "Black", 220, new DateTime(2020, 06, 07));
                yeezy350Black.AddBarcode("FY5158");
                yeezy350Black.onlineImg = "adidas-Yeezy-Boost-350-V2-Black";
                Sneaker jordan5OffWhite = new Sneaker("Jordan 5 Retro", "Black", 225, new DateTime(2020, 02, 15));
                jordan5OffWhite.AddBarcode("CT8480-001");
                jordan5OffWhite.onlineImg = "Air-Jordan-5-Retro-Off-White-Black";

                Sneaker[] sneakers = new Sneaker[] { yeezy350Zebra, jordan1Purple, supremeAF1Black, supremeAF1White, yeezySlideBone, yeezy350Black , jordan5OffWhite};
                _dbContext.AddRange(sneakers);


                /*yeezy350Zebra.AddStock(45.0, 2);
                yeezy350Zebra.AddStock(40.5, 1);
                jordan1Red.AddStock(42.0, 3);
                supremeAF1White.AddStock(38.0, 1);*/

                supreme.AddSneaker(supremeAF1Black);
                supreme.AddSneaker(supremeAF1White);
                airJordan.AddSneaker(jordan1Purple);
                adidas.AddSneaker(yeezy350Zebra);
                adidas.AddSneaker(yeezySlideBone);
                adidas.AddSneaker(yeezy350Black);
                offWhite.AddSneaker(jordan5OffWhite);

                Random rnd = new Random();
                foreach (Sneaker sneaker in sneakers)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        customer1.AddAsk(sneaker, rnd.Next(36, 47), rnd.Next(100, 350));
                    }
                }
                /*customer1.AddAsk(yeezy350Zebra, 45, 250);
                customer1.AddAsk(supremeAF1White, 45, 180);
                customer2.AddAsk(supremeAF1White, 45, 200);

                customer3.AddBid(yeezy350Zebra, 45, 230);*/
            }
            _dbContext.SaveChanges();
        }

        private async Task InitializeUsers()
        {
            string eMailAddress = "admin@hotmail.com";
            IdentityUser user = new IdentityUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));

            eMailAddress = "customer@hotmail.com";
            user = new IdentityUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "customer"));
        }
    }
}
