using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    public class RentalController : Controller
    {
        private readonly DataContext _context;

        public RentalController(DataContext ctx)
        {
            _context = ctx;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: api/values
        [HttpGet]
       /*  public IEnumerable<> Get(Rental rental)
        {
            var moviesQuery = _context.Rentals.ToList();

            return moviesQuery.ToList();
            
            
        } */

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult CreateRental(Rental rental)
        {
            /* if (_context.customers.Any(c=>c.Id==rental.CustomerId))
            {
                return _context.customers.FirstOrDefault(c=>rental.Customers==rental.CustomerId);
            }*/
            var customer = _context.customers.OfType<Customer>().Where(c=>rental.Customers.Id==rental.CustomerId).Any();
            //context.Clients.Where(c => c.Code == "test").Select(c => c.Code).FirstOrDefault();
             
               
                
            var movies = _context.movies.Where(c=>c.Id == rental.MovieId);

            foreach(var movie in movies) 
                {
                    movie.NumberAvailable--;
                    var rentals = new Rental
                    {
                        Customers = customer,
                        Movies =movie,
                        DateRented = DateTime.Now
                    };
                    _context.Rental.Add(rentals);
                }
                _context.SaveChanges();
                return Ok();
            
        } 
        

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
