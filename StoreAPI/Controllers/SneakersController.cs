using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data.Repositories;
using StoreAPI.DTO;
using StoreAPI.Models;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SneakersController : ControllerBase
    {
        private readonly IRepository<Sneaker> _sneakerRepository;
        private readonly IRepository<Brand> _brandRepository;

        public SneakersController(IRepository<Sneaker> context, IRepository<Brand> contextBrand)
        {
            _sneakerRepository = context;
            _brandRepository = contextBrand;
        }

        //GET: api/Sneakers
        /// <summary>
        /// Get all the sneakers
        /// </summary>
        /// <returns>List of sneakers</returns>
        [HttpGet]
        public IEnumerable<SneakerOutDTO> GetSneakers()
        {
            IEnumerable<SneakerOutDTO> sneakers = _sneakerRepository.GetAll().ToList().Select(sneaker => new SneakerOutDTO
            {
                Id = sneaker.Id,
                Name = sneaker.Name,
                Price = sneaker.Price,
                Brand = new BrandDTO { Name = sneaker.Brand.Name },
                Color = sneaker.Color,
                ReleaseDate = sneaker.ReleaseDate.ToString(),
                Stock = sneaker.Stock.Select(stock => new StockDTO
                {
                    Amount = stock.Amount,
                    Size = stock.Size,
                }),
                Barcode = sneaker.Barcode,
            }) ;
            return sneakers;
            
        }

        //GET: api/Sneakers/id
        /// <summary>
        /// Get the sneaker with the given id
        /// </summary>
        /// <param name="id">the id of the sneaker</param>
        /// <returns>the sneaker or 404 - Not Found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Sneaker> GetSneaker(int id)
        {
            Sneaker sneaker = _sneakerRepository.FindById(id);
            if (sneaker == null)
                return NotFound();
            return Ok(sneaker);
        }
        /*
        //GET: api/Sneakers/id
        /// <summary>
        /// Get the sneaker with the given id
        /// </summary>
        /// <param name="name">the name of the sneaker</param>
        /// <returns>list of sneakers with given name or 404 - Not Found</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Sneaker>> GetSneakerByName(string name)
        {
            IEnumerable<Sneaker> sneakers = _sneakerRepository.FindByString(name);
            if (sneakers.Count() == 0)
                return NotFound();
            return Ok(sneakers);
        }
        */
        /// <summary>
        /// Add a sneaker to the database
        /// </summary>
        /// <param name="sneakerDTO">the seaker to be added</param>
        /// <returns>201 - Created</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Sneaker> PostSneaker(SneakerDTO sneakerDTO)
        {
            Brand brand = _brandRepository.GetAll().Where(b => b.Name.Equals(sneakerDTO.Brand.Name)).FirstOrDefault();
            if(brand == null)
            {
                brand = new Brand(sneakerDTO.Brand.Name);
            }
            Sneaker sneaker = new Sneaker(sneakerDTO.Name, sneakerDTO.Color, sneakerDTO.Price, DateTime.Parse(sneakerDTO.ReleaseDate));
            sneaker.AddBarcode(sneakerDTO.Barcode);
            brand.AddSneaker(sneaker);
            _sneakerRepository.Add(sneaker);
            _sneakerRepository.SaveChanges();

            return CreatedAtAction(nameof(GetSneaker), new { id = sneaker.Id }, sneaker);
        }
        /*
        /// <summary>
        /// Add a sneaker with a brand to the database
        /// </summary>
        /// <param name="sneakerDTO">the sneaker to be added</param>
        /// <returns>201 - Created</returns>
        [HttpPost("sneakerWithBrand")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Sneaker> PostSneakerWithBrand(SneakerDTO sneakerDTO)
        {
            Brand brand = _brandRepository.FindById(sneakerDTO.BrandId);
            Sneaker sneaker = new Sneaker(sneakerDTO.Name, sneakerDTO.Color, sneakerDTO.Price, sneakerDTO.ReleaseDate, brand);
            _sneakerRepository.Add(sneaker);
            _sneakerRepository.SaveChanges();

            return CreatedAtAction(nameof(GetSneaker), new { id = sneaker.Id }, sneaker);
        }
        */
        /// <summary>
        /// UNUSED - Update a sneaker
        /// </summary>
        /// <param name="id">Id of the sneaker</param>
        /// <param name="sneaker">The sneaker that has to be updated</param>
        /// <returns>400 - Bad Request, 404 - Not Found, 204 - No Content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PutSneaker(int id, Sneaker sneaker)
        {
            if (_sneakerRepository.FindById(id) == null)
                return NotFound();
            if (id != sneaker.Id)
                return BadRequest();
            _sneakerRepository.Update(sneaker);
            _sneakerRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Delete a sneaker from the database
        /// </summary>
        /// <param name="id">Id of the sneaker</param>
        /// <returns>404 - Not Found, 204 - No Content</returns>

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteSneaker(int id)
        {
            Sneaker sneaker = _sneakerRepository.FindById(id);
            if (sneaker == null)
                return NotFound();
            _sneakerRepository.Delete(sneaker);
            _sneakerRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// UNUSED - Add a brand to a given Sneaker
        /// </summary>
        /// <param name="brandId">The id of the brand</param>
        /// <param name="sneakerId">The id of the sneaker</param>
        /// <returns>404 - Not Found, 204 - No Content</returns>
        [HttpPut("addBrand")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult AddBrand(int brandId, int sneakerId)
        {
            Sneaker sneaker = _sneakerRepository.FindById(sneakerId);
            Brand brand = _brandRepository.FindById(brandId);
            if (sneaker == null || brand == null)
                return NotFound();

            sneaker.AddBrand(brand);
            _sneakerRepository.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// UNUSED - Add stock to a given Sneaker
        /// </summary>
        /// <param name="id">The id of the sneaker</param>
        /// <param name="stock">The info about the stock</param>
        /// <returns>404 - Not Found, 204 - No Content</returns>
        [HttpPut("addStock/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult AddStock(int id, StockDTO stock)
        {
            Sneaker sneaker = _sneakerRepository.FindById(id);
            if (sneaker == null)
                return NotFound();

            sneaker.AddStock(stock.Size, stock.Amount);
            _sneakerRepository.SaveChanges();
            return NoContent();
        }


    }
}