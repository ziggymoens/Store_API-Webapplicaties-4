using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomersController(IRepository<Customer> context)
        {
            _customerRepository = context;
        }

        //GET: api/Customers
        /// <summary>
        /// UNUSED - Get all the customers
        /// </summary>
        /// <returns>List of customers</returns>
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetAll().OrderBy(c => c.LastName).ThenBy(c => c.Name);
        }

        //GET: api/Customers/id
        /// <summary>
        /// UNUSED - Get a customer with a given id
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <returns>The customer or 404 - Not Found</returns>
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> GetCustomer(int id)
        {
            Customer customer = _customerRepository.FindById(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        //GET: api/Customers/id
        /// <summary>
        /// UNUSED - Get customers with a given email
        /// </summary>
        /// <param name="email">The email of the customer</param>
        /// <returns>List of customers or 404 - Not Found</returns>
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Customer>> GetCustomerByEmail(string email)
        {
            IEnumerable<Customer> customers = _customerRepository.FindByString(email);
            if (customers.Count() == 0)
                return NotFound();
            return Ok(customers);
        }

        /// <summary>
        /// UNUSED - Add a customer to the database
        /// </summary>
        /// <param name="customerDTO">the customer to be added</param>
        /// <returns>201 - Created</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Customer> PostCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new Customer(customerDTO.Name, customerDTO.LastName, customerDTO.Email);
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }
        /// <summary>
        /// UNUSED - Update a given customer
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <param name="customer">the customer that has to be updated</param>
        /// <returns>400 - Bad Request, 404 - Not Found, 204 - No Content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PutCustomer(int id, Customer customer)
        {
            if (_customerRepository.FindById(id) == null)
                return NotFound();
            if (id != customer.Id)
                return BadRequest();
            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// UNUSED - Delete a customer from the database
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <returns>404 - Not Found, 204 - No Content</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteCustomer(int id)
        {
            Customer customer = _customerRepository.FindById(id);
            if (customer == null)
                return NotFound();
            _customerRepository.Delete(customer);
            _customerRepository.SaveChanges();
            return NoContent();
        }
    }
}