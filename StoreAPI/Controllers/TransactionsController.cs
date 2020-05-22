using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Sneaker> _sneakerRepository;
        private readonly IRepository<Bid> _bidRepository;
        private readonly IRepository<Ask> _askRepository;

        public TransactionsController(IRepository<Customer> customerRepository, IRepository<Sneaker> sneakerRepository, IRepository<Bid> bidRepository, IRepository<Ask> askRepository)
        {
            _customerRepository = customerRepository;
            _sneakerRepository = sneakerRepository;
            _askRepository = askRepository;
            _bidRepository = bidRepository;
        }

        //GET: api/Bids
        /// <summary>
        /// UNUSED - Get all the bids
        /// </summary>
        /// <returns>List of Bids</returns>
        [HttpGet("Bids")]
        public IEnumerable<Bid> GetBids()
        {
            return _bidRepository.GetAll(); ;
        }

        //GET: api/Asks
        /// <summary>
        /// UNUSED - Get all the asks
        /// </summary>
        /// <returns>List of asks</returns>
        [HttpGet("Asks")]
        public IEnumerable<Ask> GetAsks()
        {
            return _askRepository.GetAll(); ;
        }

        //GET: api/Bids/id
        /// <summary>
        /// UNUSED - Get a bid with a given id
        /// </summary>
        /// <param name="id">The id of the bid</param>
        /// <returns>The bid or 404 - Not Found</returns>
        [HttpGet("Bids/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Bid> GetBid(int id)
        {
            Bid bid = _bidRepository.FindById(id);
            if (bid == null)
                return NotFound();
            return Ok(bid);
        }

        //GET: api/Asks/id
        /// <summary>
        /// UNUSED - Get asks with a given id
        /// </summary>
        /// <param name="id">The id of the ask</param>
        /// <returns>List of customers or 404 - Not Found</returns>
        [HttpGet("Asks/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Ask> GetAsk(int id)
        {
            Ask ask = _askRepository.FindById(id);
            if (ask == null)
                return NotFound();
            return Ok(ask);
        }

    }
}