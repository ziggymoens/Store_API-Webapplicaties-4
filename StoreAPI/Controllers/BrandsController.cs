using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
    public class BrandsController : ControllerBase
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandsController(IRepository<Brand> context)
        {
            _brandRepository = context;
        }

        //GET: api/Brands
        /// <summary>
        /// Get all the brands
        /// </summary>
        /// <returns>List of brands</returns>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<BrandOutDTO> GetBrands()
        {
            IEnumerable<BrandOutDTO> brands = _brandRepository.GetAll().ToList().Select(brand => new BrandOutDTO
            {
                Name = brand.Name
   
            });
            return brands;
        }

        //GET: api/Brands/id
        /// <summary>
        /// UNUSED - Get the brand with the given id
        /// </summary>
        /// <param name="id">The id of the brand</param>
        /// <returns>A brand or 404 - Not Found</returns>
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Brand> GetBrand(int id)
        {
            Brand brand = _brandRepository.FindById(id);
            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        //GET: api/Brands/name
        /// <summary>
        /// UNUSED - Get the brand with the given name
        /// </summary>
        /// <param name="name">The name of the brand</param>
        /// <returns>List of brands or 404 - Not Found</returns>
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Brand>> GetBrand(string name)
        {
            IEnumerable<Brand> brands = _brandRepository.FindByString(name);
            if (brands.Count() == 0)
                return NotFound();
            return Ok(brands);
        }

        /// <summary>
        /// UNUSED - Add a new brand to the database
        /// </summary>
        /// <param name="brandDTO">the new brand</param>
        /// <returns>201 - Created</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<BrandDTO> PostBrand(BrandDTO brandDTO)
        {
            Brand brand = new Brand(brandDTO.Name);
            _brandRepository.Add(brand);
            _brandRepository.SaveChanges();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        /// <summary>
        /// UNUSED - Update a given brand
        /// </summary>
        /// <param name="id">The id of the brand that has to be updated</param>
        /// <param name="brand">The brand to update</param>
        /// <returns>400 - Bad Request, 404 - Not Found, 204 - No Content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PutBrand(int id, Brand brand)
        {
            if (_brandRepository.FindById(id) == null)
                return NotFound();
            if(id != brand.Id)
                return BadRequest();
            _brandRepository.Update(brand);
            _brandRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// UNUSED - Delete a brand
        /// </summary>
        /// <param name="id">the id of the brand</param>
        /// <returns>404 - Not Found, 204 - No Content</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteBrand(int id)
        {
            Brand brand = _brandRepository.FindById(id);
            if (brand == null)
                return NotFound();
            _brandRepository.Delete(brand);
            _brandRepository.SaveChanges();
            return NoContent();
        }
    }
}